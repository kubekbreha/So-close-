using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

    [Header("Options Panel")]
    public GameObject MainPanel;
    public GameObject CharacterPanel;
    public GameObject ControlsPanel;
    
    public void openOptions()
    {
        //enable respective panel
        
        ControlsPanel.SetActive(true);
        MainPanel.SetActive(false);
        

    }


    //public void newGame()
    //{
    //    if (!string.IsNullOrEmpty(newGameSceneName))
    //        SceneManager.LoadScene(newGameSceneName);
    //    else
    //        Debug.Log("Please write a scene name in the 'newGameSceneName' field of the Main Menu Script and don't forget to " +
    //            "add that scene in the Build Settings!");
    //}


    #region Back Buttons

    public void back()
    {
        ControlsPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Sound Switches
    public void Soundswitch()
    {
        bool onOffSwitch = GameObject.FindObjectOfType<Toggle>().isOn;
        if (onOffSwitch)
        {
            Debug.Log("switch is on");
        }
        else
        {
            Debug.Log("off");
        }
    }

    public void Musicswitch()
    {
        bool onOffSwitch = GameObject.FindObjectOfType<Toggle>().isOn;
        if (onOffSwitch)
        {
            Debug.Log("switch is on");
        }
        else
        {
            Debug.Log("off");
        }
    }
    #endregion

    public void startGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
