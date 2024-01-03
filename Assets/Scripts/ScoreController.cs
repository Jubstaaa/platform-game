using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

    }
    private void Update()
    {
     transform.Rotate(new Vector3(0,0.1f,0));   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.UpdateScore(50);
            Destroy(gameObject);
        }
    }

   
}
