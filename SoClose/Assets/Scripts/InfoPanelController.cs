//using UnityEngine;
//using UnityEngine.UI;

//public class InfoPanelController : MonoBehaviour
//{
//    public GameObject[] PlayerInfos;

//    public GameController gameController;

//    // Use this for initialization
//    void Start()
//    {
//        DeactivateAllInfos();

//        gameController = GameController.Instance;
//        gameController.PlayerAdded += c => UpdateScoreTable();
//        gameController.PlayerRemoved += c => UpdateScoreTable();
//        gameController.ScoreUpdated += c => UpdateScoreTable();
//    }

//    private void DeactivateAllInfos()
//    {
//        foreach (var playerInfo in PlayerInfos)
//        {
//            playerInfo.SetActive(false);
//        }
//    }

//    public void UpdateScoreTable()
//    {
//        DeactivateAllInfos();

//        var i = 0;
//        foreach (var controller in gameController.GetControllers())
//        {
//            PlayerInfos[i].SetActive(true);
//            PlayerInfos[i].GetComponentInChildren<Text>().text = controller.PlayerName + ": " + controller.Score;
//            var sprite = controller.GameObject.GetComponent<SpriteRenderer>().sprite;
//            PlayerInfos[i].GetComponentInChildren<Image>().sprite = sprite;
//            i++;
//        }
//    }
//}