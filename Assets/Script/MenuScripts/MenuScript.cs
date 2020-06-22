using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {

        Button[] mainMenuButtons = new Button[4];
        Slider[] sliders = new Slider[2];

        mainMenuButtons[0] = transform.Find("StartGameButton").GetComponent<Button>();
        mainMenuButtons[0].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Gameplay");
        });


        mainMenuButtons[1] = transform.Find("QuitButton").GetComponent<Button>();
        mainMenuButtons[1].onClick.AddListener(delegate ()
        {
            Application.Quit();
        });

        mainMenuButtons[2] = transform.Find("TrainButton").GetComponent<Button>();
        mainMenuButtons[2].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("TrainMenu");
        });


    }

}
