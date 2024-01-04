using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject chatBoxPrefab; 
    private SpriteRenderer icon;
    private Color originalColor;
    private GameObject chatBox; 


    private void Start()
    {
        icon = GetComponent<SpriteRenderer>();
        if (icon != null)
        {
            originalColor = icon.color;
        }
    }

    void Update()
    {
        if (EnemyController.totalEnemyNumber <= 0)
        {
            if (icon != null)
            {
                icon.color = originalColor;
            }
        }
        else
        {
            if (icon != null)
            {
                Color currentColor = icon.color;
                currentColor.a = 0.5f;
                icon.color = currentColor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (EnemyController.totalEnemyNumber > 0)
            {
                ShowChatBox();
            }
            else
            {
                winPanel.SetActive(true);
            }
        }
    }

    void ShowChatBox()
    {
        chatBox = Instantiate(chatBoxPrefab, transform.position + new Vector3(1.5f,1.5f,0), Quaternion.identity);
        chatBox.transform.parent = transform; 

        StartCoroutine(HideChatBoxAfterDelay());
    }

    IEnumerator HideChatBoxAfterDelay()
    {
        yield return new WaitForSeconds(3f); 
        Destroy(chatBox);
    }
}
