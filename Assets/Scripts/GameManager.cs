using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text score_text;
    public TMP_Text finalScore_text;
    public TMP_Text highScore_text;
    public TMP_Text combo_text;
    public GameObject[] life_hearts;
    private int score;
    private int combo;
    private Camera mainCamera;

    public Color[] comboColors = new Color[]
    {
        new Color(0.9f, 0.0f, 0.0f),
        new Color(0.2f, 0.6f, 0.9f),
        new Color(0.18f, 0.8f, 0.44f),
        new Color(0.95f, 0.76f, 0.06f),
        new Color(0.9f, 0.49f, 0.13f),
        new Color(0.9f, 0.3f, 0.24f),
        new Color(0.61f, 0.36f, 0.71f)
    };

    public SpriteRenderer crosshairRenderer;
    public Image buttonImage;
    public Image joyStickCenter;
    public Image joyStickHandle;
    public GameObject loseScreen;

    public AudioClip loseLifeSound;
    private AudioSource audioSource;

    private int lives = 5;

    void Start()
    {
        combo = 0;
        score = 0;
        UpdateScoreText();
        UpdateComboText();
        UpdateCrosshairColor(0);
        UpdateLives();
        loseScreen.SetActive(false);
        PlayerPrefs.SetInt("HighScore",0);

        audioSource = GetComponent<AudioSource>();

        mainCamera = Camera.main;
        float red = PlayerPrefs.GetFloat("BackgroundColor_R", 0.0f);
        float green = PlayerPrefs.GetFloat("BackgroundColor_G", 0.0f);
        float blue = PlayerPrefs.GetFloat("BackgroundColor_B", 0.0f);
        mainCamera.backgroundColor = new Color(red, green, blue);
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
        CheckHighScore();
    }

    private void CheckHighScore(){
        if(score > PlayerPrefs.GetInt("HighScore",0)){
            PlayerPrefs.SetInt("HighScore",score);
        }
        highScore_text.text = PlayerPrefs.GetInt("HighScore",0).ToString();
    }

    public void IncrementCombo()
    {
        combo++;
        UpdateComboText();
        if (combo % 3 == 0)
        {
            int bonus = combo / 3;
            score += bonus;
            UpdateScoreText();
            UpdateCrosshairColor(bonus);
        }
        CheckHighScore();
    }

    public void ResetCombo()
    {
        combo = 0;
        UpdateComboText();
        UpdateCrosshairColor(0);
    }

    public void LoseLife()
    {
        if (loseLifeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(loseLifeSound);
        }

        lives--;
        UpdateLives();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
        finalScore_text.text = score.ToString();
    }

    private void UpdateScoreText()
    {
        score_text.text = score.ToString();
    }

    private void UpdateComboText()
    {
        combo_text.text = combo.ToString();
    }

    private void UpdateLives()
    {
        switch(lives)
        {
            case 4:
                life_hearts[4].SetActive(false);
                break;
            case 3:
                life_hearts[3].SetActive(false);
                break;
            case 2:
                life_hearts[2].SetActive(false);
                break;
            case 1:
                life_hearts[1].SetActive(false);
                break;
            case 0:
                life_hearts[0].SetActive(false);
                break;
            default:
                break;
        }
    }

    private void UpdateCrosshairColor(int level)
    {
        if (level >= 0 && level < comboColors.Length)
        {
            Color selectedColor = comboColors[level];
            crosshairRenderer.color = selectedColor;
            buttonImage.color = selectedColor;
            joyStickCenter.color = selectedColor;
            joyStickHandle.color = selectedColor;
        }
    }
}
