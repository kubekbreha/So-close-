using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using UnityEngine;
using UnityEngine.Networking;

//TODO: oddelit uplne UI - nech ostane len GameController a Controller - bude mat aj podporu Score
//Mozno mat nejaky delegat na instantiate player - zavolam ho a vrati mi naspat GameObject
//Snad tiez udalosti PlayerConnected a PlayerDisconnected - na to by a tiez mohol viazat ScoreBoard
//TODO: podpora sprav v json formate
public class GameController : MonoBehaviour
{
    private const long DisconnectTimeout = 6 * 1000 * 10000;

    private const long MaxPlayers = 6;

    //Implemented as singleton
    public static GameController Instance { get; private set; }

    [SerializeField] private string brokerHost;

    [SerializeField] private int brokerPort;

    [SerializeField] private string topicPrefix;

    [SerializeField] private string scoreUrl;

    [SerializeField] private GameObject[] playersPrefabs;

    public string GameId { get; }

    public event Action<Controller> PlayerAdded;

    public event Action<Controller> PlayerRemoved;

    public event Action<Controller> ScoreUpdated;

    private Queue<int> availablePrefabsIndeces;

    private MqttClient client;

    private IDictionary<string, Controller> controllers = new Dictionary<string, Controller>();

    public GameController()
    {
        GameId = GenerateGameId();
    }

    private string GenerateGameId()
    {
        var id = "123";
        id = DateTime.Now.Ticks.ToString();
        id = id.Substring(id.Length - 4, 4);
        return id;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            availablePrefabsIndeces = new Queue<int>(Enumerable.Range(0, playersPrefabs.Length));
            Connect();
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        InstantiateConnectedPlayers();
        RemoveNotConnectedPlayers();
    }

    private void InstantiateConnectedPlayers()
    {
        foreach (var pair in controllers)
            if (pair.Value.GameObject == null)
                InstantiatePlayerGameObject(pair.Value);
    }

    private void RemoveNotConnectedPlayers()
    {
        var time = DateTime.Now.Ticks;
        var toRemove = controllers.Where(pair => time - pair.Value.LastChange > DisconnectTimeout)
            .Select(pair => pair.Key).ToList();
        foreach (var key in toRemove)
            RemovePlayer(controllers[key]);
    }

    private void InstantiatePlayerGameObject(Controller controller)
    {
        var index = availablePrefabsIndeces.Count > 0 ? availablePrefabsIndeces.Dequeue() : 0;
        var player = (GameObject) Instantiate(playersPrefabs[index], transform.position, transform.rotation);
        player.GetComponent<Player>().Id = controller.PlayerId;
        player.GetComponent<Player>().SpawnPlayer();
        controller.GameObject = player;
        controller.PrefabIndex = index;
        PlayerAdded?.Invoke(controller);
        StartCoroutine(GetScoreFromWeb(controller));
    }

    private void Connect()
    {
        client = new MqttClient(brokerHost, brokerPort, false, null, null, MqttSslProtocols.None);
        client.MqttMsgPublishReceived += OnMQTTMessageReceived;
        var clientId = DateTime.Now.Ticks.ToString();
        client.Connect(clientId);
        var topic = GetTopicName();

        client.Subscribe(new string[]
        {
            topic
        }, new byte[]
        {
            MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE
        });
    }

    private string GetTopicName() => topicPrefix + "/" + GameId + "/#";

    //private void SendMQTTMessage(string message)
    //{
    //    var bytes = Encoding.ASCII.GetBytes(message);
    //    client.Publish(GetTopicName(), bytes);
    //}

    void OnMQTTMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        var playerId = ExtractPlayerId(e.Topic);
        var message = Encoding.Default.GetString(e.Message);

        //new message - create player
        if (message == "new")
            AddPlayer(playerId, "Guest");
        else //handle other messages
        {
            Controller controller;
            if (!controllers.TryGetValue(playerId, out controller))
            {
                controller = AddPlayer(playerId, "Guest");
                //max numbers of players reached
                if (controller == null)
                    return;
            }

            //process message content
            if (message.StartsWith("name"))
            {
                var playerName = message.Substring(message.LastIndexOf(':') + 1);
                controller.PlayerName = playerName;
            }
            else if (message.StartsWith("move"))
            {
                message = message.Substring(message.LastIndexOf(':') + 1);
                var parts = message.Split(',');
                controller.DirectionVector = new Vector3(
                    float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture),
                    float.Parse(parts[1].Trim(), CultureInfo.InvariantCulture),
                    float.Parse(parts[2].Trim(), CultureInfo.InvariantCulture));
            }
            else if (message == "fire")
            {
                controller.Fire++;
            }

            //set last time of change
            controller.Alive();
        }
    }

    private Controller AddPlayer(string playerId, String playerName)
    {
        Controller controller;

        if (controllers.TryGetValue(playerId, out controller))
            return controller;

        if (controllers.Count < MaxPlayers)
        {
            controller = new Controller(playerId, playerName);
            controllers.Add(playerId, controller);

            return controller;
        }

        return null;
    }

    private void RemovePlayer(Controller controller)
    {
        availablePrefabsIndeces.Enqueue(controller.PrefabIndex);
        Destroy(controller.GameObject);
        controllers.Remove(controller.PlayerId);
        PlayerRemoved?.Invoke(controller);
        StartCoroutine(PutScoreToWeb(controller));
    }

    private string ExtractPlayerId(string topic) => topic.Substring(topic.LastIndexOf('/') + 1);

    public Controller GetController(string id)
    {
        Controller controller;
        return controllers.TryGetValue(id, out controller) ? controller : null;
    }

    //Update score of the player
    public void UpdateScore(string playerId, int value)
    {
        var controller = GetController(playerId);
        controller.Score += value;
        ScoreUpdated?.Invoke(controller);
    }

    public ICollection<Controller> GetControllers() => controllers.Values;

    void OnApplicationQuit() => client.Disconnect();

    private IEnumerator PutScoreToWeb(Controller controller)
    {
        using (UnityWebRequest www = UnityWebRequest.Put(scoreUrl + "/" + controller.PlayerName,
            controller.Score.ToString()))
        {
            www.SetRequestHeader("Content-Type", "text/plain");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log("http error " + www.responseCode);
            }
            else
            {
                //Debug.Log("Upload complete!");
            }
        }
    }

    private IEnumerator GetScoreFromWeb(Controller controller)
    {
        using (UnityWebRequest www =
            UnityWebRequest.Get(scoreUrl + "/" + controller.PlayerName))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log("http error " + www.responseCode);
            }
            else
            {
                var result = Encoding.Default.GetString(www.downloadHandler.data);
                controller.Score = int.Parse(result);
                ScoreUpdated?.Invoke(controller);
                //Debug.Log("Download complete! " + result);
            }
        }
    }
}