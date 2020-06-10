using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {

        Button[] mainMenuButtons = new Button[4];
        Slider[] sliders = new Slider[2];

        mainMenuButtons[0] = transform.Find("TrainButton").GetComponent<Button>();
        mainMenuButtons[0].onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(2);
            Debug.Log("clicked");
        });




        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            mainMenuButtons[2] = transform.Find("BackButton").GetComponent<Button>();
            sliders[0] = transform.Find("IterationsSlider").GetComponent<Slider>();
            Text iterationNumber = transform.Find("IterationNumber").GetComponent<Text>();

            sliders[1] = transform.Find("IterationsSpeedSlider").GetComponent<Slider>();
            Text iterationSpeedNumber = transform.Find("IterationSpeedNumber").GetComponent<Text>();


            mainMenuButtons[2].onClick.AddListener(delegate ()
            {
                SceneManager.LoadScene(0);
                Debug.Log("clicked");
            });

            sliders[0].onValueChanged.AddListener(delegate (float val)
            {
                iterationNumber.text = val.ToString();
            });

            sliders[1].onValueChanged.AddListener(delegate (float val)
            {
                iterationSpeedNumber.text = val.ToString();
            });
        }

        
       
    }


    public void MainMenu()
    {

    }

    public void PreferencesMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

//public class MainMenu : MonoBehaviour {

//	Button[] buttonMainMenu = new Button[4];

//	// Use this for initialization
//	void Start () {
		
//		buttonMainMenu[0] = transform.Find ("Play").GetComponent<Button> ();
//		buttonMainMenu[0].onClick.AddListener ( delegate() { 
//			Gameplay.Instance.Sounds.PlayForwardButton();
//			gameObject.SetActive(false);
//			PlayerMovement.Instance.InitPlayerMovement();
//		});

//		buttonMainMenu[1] = transform.Find ("Exit").GetComponent<Button> ();
//		buttonMainMenu[1].onClick.AddListener ( delegate(){ 
//			Gameplay.Instance.Sounds.PlayBackwardButton();
//			Application.Quit();
//		});

//		buttonMainMenu[2] = transform.Find ("Settings").GetComponent<Button> ();
//		buttonMainMenu[2].onClick.AddListener ( delegate() { 
//			SceneManager.LoadScene(1);
//			Gameplay.Instance.Sounds.PlayForwardButton();
//			gameObject.SetActive(false);
//			Gameplay.Instance.MenuCanvases.transform.Find("SettingsCanvas").gameObject.SetActive(true);
//		});

//		buttonMainMenu[3] = transform.Find ("Tutorial").GetComponent<Button> ();
//		buttonMainMenu[3].onClick.AddListener ( delegate() { 
//			SaveManager.Instance.data.IsTutorialTurnedOff = !SaveManager.Instance.data.IsTutorialTurnedOff;
//			ReloadTutorialText();
//			Gameplay.Instance.Sounds.PlayForwardButton();
//		});

//	}

//}
