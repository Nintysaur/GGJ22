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


    private void Awake()
    {
        Invertables = new List<BaseInvertable>();
        Invertables.Add(playerObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BaseInvertable.inverted && inversionExpiresAt < Time.time)
        {
            InvertWorlds();
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
        }
        else
        {
            BaseInvertable.inverted = true;
        }

        print("Inverted");
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
