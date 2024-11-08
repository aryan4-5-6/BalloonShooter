using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject howToPlayCanvas;
    public GameObject bgColorCanvas;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleOptions()
    {
        if (optionsCanvas != null)
        {
            optionsCanvas.SetActive(!optionsCanvas.activeSelf);
            gameObject.SetActive(false);
        }
    }
    
    public void ToggleHowToPlay()
    {
        if (howToPlayCanvas != null)
        {
            howToPlayCanvas.SetActive(!howToPlayCanvas.activeSelf);
            gameObject.SetActive(false);
        }
    }
    
    public void ToggleBGColor()
    {
        if (bgColorCanvas != null)
        {
            bgColorCanvas.SetActive(!bgColorCanvas.activeSelf);
            gameObject.SetActive(false);
        }
    }

    public void OptionsBackBtn(){
        if (optionsCanvas != null)
        {
            optionsCanvas.SetActive(!optionsCanvas.activeSelf);
            gameObject.SetActive(true);
        }
    }

    public void HowToPlayBackBtn(){
        if (howToPlayCanvas != null)
        {
            howToPlayCanvas.SetActive(!howToPlayCanvas.activeSelf);
            gameObject.SetActive(true);
        }
    }

    public void BGColorBackBtn(){
        if (bgColorCanvas != null)
        {
            bgColorCanvas.SetActive(!bgColorCanvas.activeSelf);
            gameObject.SetActive(true);
        }
    }
}
