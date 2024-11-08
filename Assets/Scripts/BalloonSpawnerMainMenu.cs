using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnerMainMenu : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] balloonPrefab;

    private float spawnInterval = 0.5f;
    private float balloonSpeed = 4.5f;   
    private float difficultyTimer;
    [SerializeField] private int difficultyLevel = 0;

    void Start()
    {
        InvokeRepeating("SpawnBalloon", 1f, spawnInterval);
    }

    void SpawnBalloon()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject balloonToSpawn = balloonPrefab[Random.Range(0, balloonPrefab.Length)];
        GameObject balloon = Instantiate(balloonToSpawn, spawnPoint.position, Quaternion.identity);
        BalloonMovement movement = balloon.GetComponent<BalloonMovement>();

        if (movement != null)
        {
            movement.InitializeMovement(spawnPoint);
            movement.SetSpeed(balloonSpeed);
        }
    }
}
