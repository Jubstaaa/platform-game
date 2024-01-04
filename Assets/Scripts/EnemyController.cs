using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MovementType
{
    LeftRight,
    UpDown,
    Cross
}
public class EnemyController : MonoBehaviour
{    public MovementType movementType;

    [SerializeField] bool onGround ;
    private float width;
    private Vector3 enemyPosition;
    private Rigidbody2D rb;
    [SerializeField] LayerMask obstacle;
    public static int totalEnemyNumber=0;
    [SerializeField] float speed = 2f;
    [SerializeField] float moveDuration = 1f; 

    bool isMovingPos = true;

    void Start()
    {
        totalEnemyNumber++;
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        
        switch (movementType)
        {
            case MovementType.LeftRight:
                StartCoroutine(VerticalMove());
                break;
            case MovementType.UpDown:
                StartCoroutine(HorizontalMove());
                break;
            case MovementType.Cross:
                StartCoroutine(CrossMove());
                break;
            default:
                break;
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveDuration); 

            isMovingPos = !isMovingPos;
        }
    }

    IEnumerator HorizontalMove()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        while (true)
        {
            if (isMovingPos)
            {
                rb.velocity = Vector2.right * speed; 
            }
            else
            {
                rb.velocity = Vector2.left * speed; 
            }

            yield return new WaitForSeconds(moveDuration);

            rb.velocity = Vector2.zero; 
        }
    }

    IEnumerator VerticalMove()
    {
        rb.gravityScale = 0f; 

        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        while (true)
        {
            if (isMovingPos)
            {
                rb.velocity = Vector2.up * speed; 
            }
            else
            {
                rb.velocity = Vector2.down * speed;
            }

            yield return new WaitForSeconds(moveDuration);

            rb.velocity = Vector2.zero; 
        }
    }

    IEnumerator CrossMove()
    {
        rb.gravityScale = 0f;

        while (true)
        {
            if (isMovingPos)
            {
                rb.velocity = new Vector2(speed, speed);
            }
            else
            {
                rb.velocity = new Vector2(-speed, -speed);
            }

            yield return new WaitForSeconds(moveDuration);

            rb.velocity = Vector2.zero;
          

        }
    }



    private void OnDestroy()
    {
        totalEnemyNumber--;
    }

    
}
