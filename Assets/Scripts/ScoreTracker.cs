using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker
{
    static int scoreValue;

    public ScoreTracker()
    {
        scoreValue = 0;
    }

    public static void AddScore(int value)
    {
        scoreValue += value;
    }

    public static int GetScore()
    {
        return scoreValue;
    }
}
