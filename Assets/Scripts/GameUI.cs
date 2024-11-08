using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject gameCanvas;

    public void PauseBtn(){
        if (pauseCanvas != null)
        {
            Time.timeScale = 0f;
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            gameCanvas.SetActive(!gameCanvas.activeSelf);
        }
    }

    public void ContBtn(){
        if (pauseCanvas != null)
        {
            Time.timeScale = 1f;
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            gameCanvas.SetActive(!gameCanvas.activeSelf);
        }
    }

    public void Exit(){
        SceneManager.LoadScene(0);
    }

    public void RestartBtn(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
