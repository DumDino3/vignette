using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerSpeed = 11;
    public float dashStrength = 25;

    public Vector2 mouseWorldPos;

    //private bool staggering = false;
    //public float staggered;
    //public float knockbackX;
    //public float knockbackY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 thisPos = this.transform.position;

        float targetX = mouseWorldPos.x - thisPos.x;
        float targetY = mouseWorldPos.y - thisPos.y;

        float rotationValue = Mathf.Atan2(targetY, targetX) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationValue));

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movementInput.sqrMagnitude > 0.01f)
        {
            Vector2 velocity = movementInput.normalized * playerSpeed;
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
