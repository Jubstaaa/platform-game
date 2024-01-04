using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        GameObject[] mapBounds = GameObject.FindGameObjectsWithTag("MapBounds");

        if (mapBounds.Length > 0)
        {

            foreach (GameObject bound in mapBounds)
            {
                Renderer renderer = bound.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Bounds bounds = renderer.bounds;
                    minX = Mathf.Min(minX, bounds.min.x);
                    maxX = Mathf.Max(maxX, bounds.max.x);
                    minY = Mathf.Min(minY, bounds.min.y);
                    maxY = Mathf.Max(maxY, bounds.max.y);
                }
            }
        }



        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(playerTransform.position.x, minX+9.6f, maxX - 9.6f),
            Mathf.Clamp(playerTransform.position.y, minY+5.4f, maxY - 5.4f),
            transform.position.z
        );
    }
}
