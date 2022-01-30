using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TopDownMovement))]
public class PlayerController : BaseInvertable, IMortalObject
{
    private TopDownMovement movement;
    private GameController gc;

    public bool Dead { get; set; }
    public int HealthMax = 5;
    public int HealthCurrent;

    [SerializeField] GameObject projectile;
    [SerializeField] float reloadTime = 0.6f;
    float reloadedAt;

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<TopDownMovement>();
        Initialise();

        HealthCurrent = HealthMax;

        gc = FindObjectOfType<GameController>();

        if (anim == null)
        {
            print("No animator found!");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleAnims();
    }

    void HandleInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movement.Move(input);

        if (Input.GetMouseButtonDown(0) && Time.time > reloadedAt)
        {
            Plane plane = new Plane(Vector3.forward, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                Vector2 heading = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

                Shoot(heading);
            }

            reloadedAt = Time.time + reloadTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gc.RequestWorldInversion();
        }
    }

    void HandleAnims()
    {
        //Speed
        float speed = movement.GetSpeed();
        anim.SetFloat("Speed", speed);

        //Are we firing? anim lasts for like 0.2f
        if (Time.time >= (reloadedAt - reloadTime) + 0.08f)
        {
            anim.SetBool("IsAttacking", false);
        }

    }

    protected void Shoot(Vector2 heading)
    {
        GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile proj = obj.GetComponent<Projectile>();
        obj.transform.up = heading;

        GameController.RegisterInvertable(proj);
        proj.caster = gameObject;

        anim.SetBool("IsAttacking", true);
    }

    public void Damage(int value)
    {
        HealthCurrent -= value;

        if (HealthCurrent <= 0)
        {
            HealthCurrent = 0;
            Die();
        }
    }

    public void Heal(int value)
    {
        HealthCurrent += value;

        if (HealthCurrent >= HealthMax)
        {
            HealthCurrent = HealthMax;
        }
    }

    public void Die()
    {
        if (!gc.RequestWorldInversion())
        {
            Dead = true;
            //Game Over
            print("GameOver");
        }
    }

    public int GetHealth()
    {
        return HealthCurrent;
    }
}
