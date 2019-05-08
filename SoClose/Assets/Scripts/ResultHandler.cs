using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ResultHandler : MonoBehaviour
{

    public Button newGameButton;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(ReloadScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
