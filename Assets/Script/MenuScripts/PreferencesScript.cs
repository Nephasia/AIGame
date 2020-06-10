using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreferencesScript : MonoBehaviour
{

    public static string iterNum;


    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Button[] mainMenuButtons = new Button[4];
        Slider[] sliders = new Slider[2];

        mainMenuButtons[0] = transform.Find("TrainButton").GetComponent<Button>();
        mainMenuButtons[0].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("TrainMenu");
            Debug.Log("clicked");
        });

        mainMenuButtons[2] = transform.Find("BackButton").GetComponent<Button>();
        sliders[0] = transform.Find("IterationsSlider").GetComponent<Slider>();
        Text iterationNumber = transform.Find("IterationNumber").GetComponent<Text>();

        sliders[1] = transform.Find("IterationsSpeedSlider").GetComponent<Slider>();
        Text iterationSpeedNumber = transform.Find("IterationSpeedNumber").GetComponent<Text>();


        mainMenuButtons[2].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Gameplay");
            Debug.Log("clicked");
        });

        sliders[0].onValueChanged.AddListener(delegate (float val)
        {
            iterationNumber.text = val.ToString();
            iterNum = iterationNumber.text;
        });

        sliders[1].onValueChanged.AddListener(delegate (float val)
        {
            iterationSpeedNumber.text = val.ToString();
        });
    }
}
