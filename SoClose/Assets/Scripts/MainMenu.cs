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

    public GameObject Button;
    public GameObject Button1;
    public GameObject Button2;
    private int positionPerson = 0;



    public void Start()
    {
        Button.gameObject.SetActive(true);
        Button1.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);

    }

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


     
    public void changePersonLeft()   //zmena pozicie dolava postavicky v menu
    {
        switch (positionPerson)
        {
            case 0:
                positionPerson = 2;
                Button.gameObject.SetActive(false);
                Button2.gameObject.SetActive(true);
                break;
            case 1:
                positionPerson = 0;
                Button1.gameObject.SetActive(false);
                Button.gameObject.SetActive(true);
                break;
            case 2:
                positionPerson = 1;
                Button2.gameObject.SetActive(false);
                Button1.gameObject.SetActive(true);
                break;
        }
        PlayerPrefs.SetInt("Characters", positionPerson);
    }

    public void changePersonRight()             //zmena pozicie doprava postavicky v menu
    {
    
        switch (positionPerson)
        {
            case 0:
                positionPerson = 1;
                Button.gameObject.SetActive(false);
                Button1.gameObject.SetActive(true);
                break;
            case 1:
                positionPerson = 2;
                Button1.gameObject.SetActive(false);
                Button2.gameObject.SetActive(true);
                break;
            case 2:
                positionPerson = 0;
                Button2.gameObject.SetActive(false);
                Button.gameObject.SetActive(true);
                break;
        }

        PlayerPrefs.SetInt("Characters", positionPerson);
    }

}
