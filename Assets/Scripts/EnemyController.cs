using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool onGround ;
    private float width;
    private Vector3 enemyPosition;
    private Rigidbody2D rb;
    [SerializeField] LayerMask obstacle;
    private static int totalEnemyNumber=0;
    void Start()
    {
        totalEnemyNumber++;
        Debug.Log(totalEnemyNumber);
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width / 2), Vector2.down,2f,obstacle);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        if (hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

       

        Flip();

    }


    private void Flip()
    {
        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }

        rb.velocity = new Vector2(transform.right.x * 2f, 0f);
    }
}
