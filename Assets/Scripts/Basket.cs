using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    Rigidbody rb;
    bool isClicked;
    Vector3 offset = new Vector3(0, -4, 0);
    float xBounds = 18;
    float moveSpeed = 300f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseUp()
    {
        isClicked = false;
    }

    void FixedUpdate()
    {
        if (isClicked)
        {
            Vector3 destination = GameManager.Singleton.mousePos + offset;
            Vector3 direction = destination - transform.position;
            rb.velocity = direction * moveSpeed * Time.deltaTime;
        }
        else if (!isClicked)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }
    }
}
