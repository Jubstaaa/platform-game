using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    private Vector3 ls;
    public bool onGround;
    private bool canDoubleJump;
    [SerializeField] GameObject arrow;
    private bool canFire = true;
    private Animator myAnimator;
    private int arrowAmmo = 3;
    [SerializeField] TextMeshProUGUI arrowAmmoText;
    [SerializeField] AudioClip deathMusic;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject winPanel,losePanel;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ls = transform.localScale;
        myAnimator = GetComponent<Animator>();
        arrowAmmoText.text = arrowAmmo.ToString();

    }

    void Update()
    {
        
        Move();

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();


        }

        if (Input.GetKeyDown(KeyCode.Space) && canFire && arrowAmmo > 0)
        {
            myAnimator.SetTrigger("Attack");
            Invoke("FireArrow", 0.5f);
            canFire = false;
            Invoke("ResetFire", 1.0f); 
        }


    }

    private void FireArrow()
    {
        GameObject arrowObject = Instantiate(arrow, transform.position, Quaternion.identity);
        arrowObject.transform.parent = GameObject.Find("Arrows").transform;
        Rigidbody2D arrowRb = arrowObject.GetComponent<Rigidbody2D>();
        arrowObject.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        arrowRb.velocity = new Vector2(10f * transform.localScale.x, 0);
        UpdateArrow(-1);
    }

    private void Jump()
    {
        if (onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            canDoubleJump = true;
            myAnimator.SetTrigger("Jump");
        }
        else
        {
            if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
                canDoubleJump = false;

            }
        }
       

    }

    private void Move()
    {
        mySpeedX = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("mySpeedX", Mathf.Abs(mySpeedX));


        rb.velocity = new Vector2(mySpeedX * moveSpeed, rb.velocity.y);

        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(ls.x, ls.y, ls.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-ls.x, ls.y, ls.z);
        }

    }

    private void ResetFire()
    {
        canFire = true;
    }

    private void Die()
    {
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip=null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(deathMusic);
        myAnimator.SetFloat("mySpeedX",0);
        myAnimator.SetTrigger("Death");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        StartCoroutine(SetLosePanel());
    }

    IEnumerator SetLosePanel() {

        yield return new WaitForSecondsRealtime(2f);
         losePanel.SetActive(true);
    }

    public void UpdateArrow(int number)
    {
        arrowAmmo +=number;
        arrowAmmoText.text = arrowAmmo.ToString();
    }

    public void UpdateScore(int point)
    {
        int scoreValue = int.Parse(scoreText.text);
        scoreValue += point;
        scoreText.text = scoreValue.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            Die();

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Finish")
        {
            winPanel.SetActive(true);
        }
    }
}
