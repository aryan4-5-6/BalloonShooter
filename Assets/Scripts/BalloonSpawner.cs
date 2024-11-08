using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] balloonPrefab;
    public GameObject iceBalloonPrefab;
    public GameObject iceEffect;

    private float spawnInterval = 2.5f;
    private float balloonSpeed = 6f;   
    private float difficultyTimer;
    private float iceTimer;
    private int difficultyLevel;

    private bool isIceEffectActive = false;
    private float iceEffectDuration = 5f;
    private float originalBalloonSpeed;
    private Shooting shooting;

    void Start()
    {
        shooting = FindObjectOfType<Shooting>();
        if (GameSettings.Instance != null)
        {
            difficultyLevel = GameSettings.Instance.difficultyLevel;
        }
        else
        {
            difficultyLevel = 0;
        }
        ApplyInitialDifficultySettings();
        difficultyTimer = Time.time;
        iceTimer = Time.time;
        InvokeRepeating("SpawnBalloon", 1f, spawnInterval);
    }

    void Update()
    {
        difficultyTimer += Time.deltaTime;
        iceTimer += Time.deltaTime;
        if(difficultyTimer >= 20f){
            IncreaseDifficulty();
            difficultyTimer = 0;
        }
        if (isIceEffectActive && iceTimer >= iceEffectDuration)
        {
            DeactivateIceEffect();
            iceTimer = 0;
        }
    }

    public void SliderDifficulty(float level)
    {
        difficultyLevel = Mathf.RoundToInt(level);
        UpdateDifficultySettings();
    }

    void ApplyInitialDifficultySettings()
    {
        switch (difficultyLevel)
        {
            case 1: // Medium
                spawnInterval = 3.5f;
                balloonSpeed = 4f;
                break;
            case 2: // Hard
                spawnInterval = 3.0f;
                balloonSpeed = 4.5f;
                break;
            case 3: // Very Hard
                spawnInterval = 2.0f;
                balloonSpeed = 6f;
                break;
            default: // Easy
                spawnInterval = 4f;
                balloonSpeed = 3f;
                break;
        }
        originalBalloonSpeed = balloonSpeed;
        CancelInvoke("SpawnBalloon");
        InvokeRepeating("SpawnBalloon", 0f, spawnInterval);
    }

    void IncreaseDifficulty()
    {
        if(difficultyLevel < 3) difficultyLevel++;
        else difficultyLevel = 3;
        UpdateDifficultySettings();
    }

    void UpdateDifficultySettings()
    {
        switch (difficultyLevel)
        {
            case 1: // Medium
                spawnInterval = 3.5f;
                balloonSpeed = 4f;
                break;
            case 2: // Hard
                spawnInterval = 3.0f;
                balloonSpeed = 4.5f;
                break;
            case 3: // Very Hard
                spawnInterval = 2.0f;
                balloonSpeed = 6f;
                break;
            default: // Easy
                spawnInterval = 4f;
                balloonSpeed = 3f;
                break;
        }
        originalBalloonSpeed = balloonSpeed;
        CancelInvoke("SpawnBalloon");
        InvokeRepeating("SpawnBalloon", 0f, spawnInterval);
    }

    void SpawnBalloon()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject balloonToSpawn;
        if (Random.value < 0.1f)
        {
            balloonToSpawn = iceBalloonPrefab;
        }
        else
        {
            balloonToSpawn = balloonPrefab[Random.Range(0, balloonPrefab.Length)];
        }

        GameObject balloon = Instantiate(balloonToSpawn, spawnPoint.position, Quaternion.identity);
        BalloonMovement movement = balloon.GetComponent<BalloonMovement>();

        if (movement != null)
        {
            movement.InitializeMovement(spawnPoint);
            movement.SetSpeed(balloonSpeed);
        }
    }

    public void ActivateIceEffect()
    {
        if(!isIceEffectActive){
            isIceEffectActive = true;
            iceEffect.SetActive(true);
            Time.timeScale /= 2f;
            // balloonSpeed = originalBalloonSpeed / 2;
            shooting.movementSpeed *= 2f;
        }
    }

    void DeactivateIceEffect()
    {
        isIceEffectActive = false;
        Debug.Log(isIceEffectActive);
        iceEffect.SetActive(false);
        Time.timeScale *= 2f;
        // balloonSpeed = originalBalloonSpeed;
        shooting.movementSpeed /= 2f;
    }
}
