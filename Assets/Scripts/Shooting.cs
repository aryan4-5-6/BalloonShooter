using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private PlayerActionsExample playerInput;
    private Vector2 movementInput;
    public GameManager gameManager;

    public Transform movableObject;
    public float movementSpeed = 5f;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public Camera cam;
    public float shootingRange = 100f;
    public LayerMask balloonLayer;
    public AudioClip shootSound;
    public AudioClip popSound;
    public GameObject popEffect;
    public float shootCooldown = 0.2f;
    private float lastShootTime;

    private AudioSource audioSource;
    private BalloonSpawner balloonSpawner;

    void Awake()
    {
        playerInput = new PlayerActionsExample();
        audioSource = GetComponent<AudioSource>();
        balloonSpawner = FindObjectOfType<BalloonSpawner>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        float shootPress = playerInput.Player.Shoot.ReadValue<float>();
        if (shootPress > 0 && Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
        MoveObject();
    }

    private void MoveObject()
    {
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0) * movementSpeed * Time.deltaTime;
        Vector3 newPosition = movableObject.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
        movableObject.position = newPosition;
    }

    void Shoot()
    {
        if (shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Vector2 origin = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector2 direction = Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, shootingRange, balloonLayer);
        if (hit.collider != null && hit.collider.CompareTag("Balloon"))
        {
            if (popSound)
            {
                audioSource.PlayOneShot(popSound);
            }

            if (popEffect)
            {
                Instantiate(popEffect, hit.point, Quaternion.identity);
            }
            Destroy(hit.collider.gameObject);
            gameManager.AddScore();
            gameManager.IncrementCombo();
            return;
        }
        else if (hit.collider != null && hit.collider.CompareTag("IceBalloon")){
             if (popSound)
            {
                audioSource.PlayOneShot(popSound);
            }

            if (popEffect)
            {
                Instantiate(popEffect, hit.point, Quaternion.identity);
            }
            Debug.Log("ICEEEEEEE");
            if(balloonSpawner) balloonSpawner.ActivateIceEffect();
            Destroy(hit.collider.gameObject);
            gameManager.AddScore();
            gameManager.IncrementCombo();
            return;
        }
        gameManager.ResetCombo();
    }
}
