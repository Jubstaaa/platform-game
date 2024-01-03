using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    private PlayerController playerController;
    public bool isCollectable;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Monster"))
        {
            playerController.UpdateScore(25);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Instantiate(deathEffect, collision.gameObject.transform.position, Quaternion.identity);

        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Rigidbody2D arrowRb = GetComponent<Rigidbody2D>();
            arrowRb.constraints = RigidbodyConstraints2D.FreezeAll;
            isCollectable = true;
        }
        else if (collision.gameObject.CompareTag("Player") && isCollectable)
        {
            playerController.UpdateArrow(1);
            Destroy(gameObject);
        }

    }

}
