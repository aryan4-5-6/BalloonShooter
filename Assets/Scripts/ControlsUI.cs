using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public GameObject controlsPC;
    public GameObject controlsMobile;

    private void Awake(){
        Time.timeScale = 0f;
    }

    public void PCBtn(){
        controlsPC.SetActive(!controlsPC.activeSelf);
        Time.timeScale = 1f;
    }

    public void MobileBtn(){
        controlsMobile.SetActive(!controlsMobile.activeSelf);
        Time.timeScale = 1f;
    }
}
