using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDownMovement))]
public class EnemyController : BaseInvertable, IMortalObject
{
    TopDownMovement movement;
    Transform target;

    [SerializeField] float shootCoolDown = 2.0f;
    float shotReady;

    [SerializeField] private GameObject Projectile;
    public bool Dead { get; set; }

    public float tombstoneDuration = 12.0f;
    float tombstoneExpiresAt = 0.0f;

    [SerializeField] int killScore = 100;
    [SerializeField] int eatScore = 40;

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
        if (!Dead)
        {
            ProcessBehaviour();
        }
        else
        {
            movement.Move(Vector2.zero);
            
            if (Time.time > tombstoneExpiresAt)
            {
                GameController.RemoveInvertable(this);
                Destroy(gameObject);
            }
        }        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (inverted && Dead && (player != null))
        {
            //if its the player
            player.Heal(1);

            ScoreTracker.AddScore(eatScore);

            GameController.RemoveInvertable(this);
            Destroy(gameObject);
        }
    }

    protected override void SwitchToInvertedWorld()
    {
        if (Dead)
        {
            sr.sprite = invertedSprite;
        }
    }

    public void Damage(int value)
    {
        Die();
    }

    public void Heal(int value)
    {
        //Not called
    }

    public void Die()
    {
        Dead = true;

        ScoreTracker.AddScore(killScore);

        //enter tombstone state, we want the bodies to linger for a while so we can grab them during inversion
        tombstoneExpiresAt = Time.time + tombstoneDuration;
    }
}
