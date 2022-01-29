using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDownMovement))]
public class Projectile : BaseInvertable
{
    TopDownMovement movement;

    public GameObject caster;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<TopDownMovement>();
        Initialise();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.Move(transform.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == caster)
        {
            return;
        }

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null) //If the thing we hit was the player
        {
            print("Hit player");
        }

        print("Hit");
        Destroy(this);
    }
}
