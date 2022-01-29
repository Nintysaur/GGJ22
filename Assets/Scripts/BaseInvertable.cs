using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseInvertable : MonoBehaviour
{ 
    [SerializeField]
    protected Sprite normalSprite;

    [SerializeField]
    protected Sprite invertedSprite;

    protected SpriteRenderer sr;

    public static bool inverted = false;

    protected void Initialise()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;
    }
    
    public void Invert()
    {
        if (inverted)
        {
            SwitchToNormalWorld();
        }
        else
        {
            SwitchToInvertedWorld();
        }
    }

    protected virtual void SwitchToNormalWorld()
    {
        sr.sprite = normalSprite;
    }

    protected virtual void SwitchToInvertedWorld()
    {
        sr.sprite = invertedSprite;
    }
}
