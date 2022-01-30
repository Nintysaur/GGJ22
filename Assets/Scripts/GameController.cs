using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static List<BaseInvertable> Invertables;

    [SerializeField] BaseInvertable playerObject;    
    [SerializeField] float inversionDuration = 5.0f;
    [SerializeField] float inversionCoolDown = 15.0f;

    float inversionReadyAt = 0.0f;
    float inversionExpiresAt = 0.0f;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnCooldown = 5.0f;
    float nextSpawnAt = 0.0f;

    public bool spawnCheck = true;

    [SerializeField] Material materialReference; 

    System.Random rnd;

    ScoreTracker score;
    [SerializeField] int scorePerSecond = 1;
    private float nextTickAt = 0.0f;

    private void Awake()
    {
        Invertables = new List<BaseInvertable>();
        Invertables.Add(playerObject);

        score = new ScoreTracker();
    }

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        materialReference.SetFloat("_Threshold", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (BaseInvertable.inverted && inversionExpiresAt < Time.time)
        {
            InvertWorlds();
        }

        //try to spawn a new enemy +/- 15/7
        //pick a spot within level
        if (spawnCheck && Time.time > nextSpawnAt)
        {
            Vector2 position = new Vector2(rnd.Next(-15, 16), rnd.Next(-7, 8));

            //if this is too close to the player, we'll try again next frame
            Vector3 v = playerObject.gameObject.transform.position;
            Vector2 playerPos = new Vector2(v.x, v.y);

            float distance = Vector2.Distance(position, playerPos);
            //print(distance);

            if (distance >= 5.0f)
            {
                Instantiate(enemyPrefab, position, Quaternion.identity);

                nextSpawnAt += spawnCooldown;
            }
        }

        //Tick Score with time
        if (Time.time >= nextTickAt)
        {
            ScoreTracker.AddScore(1);
            nextTickAt = Time.time + 1;
        }
       
    }

    private void InvertWorlds()
    {
        bool check = BaseInvertable.inverted;

        foreach (BaseInvertable i in Invertables)
        {
            i.Invert();
        }

        //invert the staic bool after
        if (check)
        {
            BaseInvertable.inverted = false;
            materialReference.SetFloat("_Threshold", 0.0f);
        }
        else
        {
            BaseInvertable.inverted = true;
            materialReference.SetFloat("_Threshold", 0.85f);
        }

        //print("Inverted");
    }

    public bool RequestWorldInversion()
    {
        float time = Time.time;
        
        //Check if inversion is ready
        if (time > inversionReadyAt)
        {
            InvertWorlds();
            inversionReadyAt = time + inversionCoolDown;
            inversionExpiresAt = time + inversionDuration;

            return true;
        }

        return false;
    }


    public static void RegisterInvertable(BaseInvertable newInvertable )
    {
        Invertables.Add(newInvertable);
    }

    public static void RemoveInvertable(BaseInvertable oldInvertable)
    {
        Invertables.Remove(oldInvertable);
    }
}
