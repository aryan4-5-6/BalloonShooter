using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundColor : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        redSlider.value = mainCamera.backgroundColor.r;
        greenSlider.value = mainCamera.backgroundColor.g;
        blueSlider.value = mainCamera.backgroundColor.b;
        redSlider.onValueChanged.AddListener(delegate { UpdateBackgroundColor(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateBackgroundColor(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateBackgroundColor(); });
    }

    void UpdateBackgroundColor()
    {
        float red = redSlider.value;
        float green = greenSlider.value;
        float blue = blueSlider.value;
        mainCamera.backgroundColor = new Color(red, green, blue);

        PlayerPrefs.SetFloat("BackgroundColor_R", red);
        PlayerPrefs.SetFloat("BackgroundColor_G", green);
        PlayerPrefs.SetFloat("BackgroundColor_B", blue);
        PlayerPrefs.Save();
    }
}
