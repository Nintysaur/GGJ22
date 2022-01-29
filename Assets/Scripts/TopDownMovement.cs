using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement: MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }
        else
        {
            direction = Vector2.zero;
        }

        rb.velocity = direction * moveSpeed;
    }
}
