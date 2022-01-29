using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDownMovement))]
public class EnemyController : BaseInvertable
{
    TopDownMovement movement;
    Transform target;

    [SerializeField] float shootCoolDown = 2.0f;
    float shotReady;

    [SerializeField] private GameObject Projectile;
    
    // Start is called before the first frame update
    void Start()
    {
        GameController.RegisterInvertable(this);
        movement = GetComponent<TopDownMovement>();

        target = GameObject.Find("Player").transform;
        if (!target)
        {
            print("Could not find player!");
        }

        Initialise();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessBehaviour();
    }
    private void ProcessBehaviour()
    {
        if (!inverted)
        {
            //Normal

            //Move toward player
            //Get direction of player relative to me
            Vector2 heading = target.position - transform.position;
            movement.Move(heading);

            //Shoot at player
            if (Time.time > shotReady)
            {
                Shoot(heading);
                shotReady = Time.time + shootCoolDown;
            }
            
        }
        else
        {
            //Inverted
            movement.Move(Vector2.zero);
            shotReady = Time.time + shootCoolDown;
        }
    }

    protected void Shoot(Vector2 heading)
    {
        GameObject obj = Instantiate(Projectile, transform.position, Quaternion.identity);
        Projectile proj = obj.GetComponent<Projectile>();
        obj.transform.up = heading;

        GameController.RegisterInvertable(proj);
        proj.caster = gameObject;
    }
}
