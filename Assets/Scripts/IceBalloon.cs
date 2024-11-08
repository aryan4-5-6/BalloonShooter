using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalloon : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boundary"))
        {
            if(gameManager) gameManager.LoseLife();
            Destroy(gameObject);
        }
    }
}
