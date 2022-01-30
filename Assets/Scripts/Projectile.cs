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
        if (!inverted)
        {
            movement.Move(transform.up);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == caster)
        {
            return;
        }

        //Try to find something we can damage
        IMortalObject target = collision.gameObject.GetComponent<IMortalObject>();
        if (target != null && !target.Dead) //If the thing we hit can be damaged
        {
            print("Hit Mortal Object");
            target.Damage(1);
        }

        //print("Hit");
        GameController.RemoveInvertable(this);
        Destroy(gameObject);
    }
}
