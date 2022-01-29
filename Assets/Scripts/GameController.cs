using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static List<BaseInvertable> Invertables;

    [SerializeField] BaseInvertable playerObject;

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
        //TEMPORARY
        if (Input.GetKeyDown(KeyCode.Space))
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
