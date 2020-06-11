using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreferencesScript : MonoBehaviour
{

    public static string iterNum = "1";

    public static string iterSpeed = "1";


    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Button[] mainMenuButtons = new Button[4];
        Slider[] sliders = new Slider[2];
        InputField[] inputs = new InputField[2];

        sliders[0] = transform.Find("IterationsSlider").GetComponent<Slider>();
        //Text iterationNumber = transform.Find("IterationNumber").GetComponent<Text>();

        sliders[1] = transform.Find("IterationsSpeedSlider").GetComponent<Slider>();
        //Text iterationSpeedNumber = transform.Find("IterationSpeedNumber").GetComponent<Text>();

        inputs[0] = transform.Find("InputIterationNumber").GetComponent<InputField>();
        inputs[0].text = "1";
        inputs[1] = transform.Find("InputIterationSpeed").GetComponent<InputField>();
        inputs[1].text = "1";

        mainMenuButtons[0] = transform.Find("TrainButton").GetComponent<Button>();
        mainMenuButtons[2] = transform.Find("BackButton").GetComponent<Button>();
        
        inputs[0].onValueChanged.AddListener(delegate (string text) {

            if(text=="" || text ==" ")
            {
                text = "1";
            }
            //iterationNumber.text = text.ToString();
            sliders[0].value = uint.Parse(text);
            iterNum = text;
            Debug.Log("changed");
        });

        inputs[1].onValueChanged.AddListener(delegate (string text) {

            if (text == "" || text == " ")
            {
                text = "1";
            }
            //iterationSpeedNumber.text = text.ToString();
            sliders[1].value = uint.Parse(text);
            iterSpeed = text;
            Debug.Log("changed");
        });

        mainMenuButtons[0].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Gameplay");
            Debug.Log("clicked");
        });

        mainMenuButtons[2].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("StartMenu");
            Debug.Log("clicked");
        });

        sliders[0].onValueChanged.AddListener(delegate (float val)
        {
            //iterationNumber.text = val.ToString();
            inputs[0].text = val.ToString();
            iterNum = val.ToString();
        });

        sliders[1].onValueChanged.AddListener(delegate (float val)
        {
            //iterationSpeedNumber.text = val.ToString();
            inputs[1].text = val.ToString();
            iterSpeed = val.ToString();
        });
    }
}
