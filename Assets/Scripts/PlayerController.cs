using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseInvertable
{
    private TopDownMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<TopDownMovement>();
        Initialise();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        if (input.magnitude > 0.1f)
        {
            movement.Move(input);
        }
        else
        {
            movement.Move(Vector2.zero);
        }

        
    }
}
