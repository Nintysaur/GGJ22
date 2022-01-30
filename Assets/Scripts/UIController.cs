using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] Image[] HealthCounter;
    [SerializeField] Image SpiritDisplay;
    [SerializeField] TMP_Text scoreDisplay;

    [SerializeField] Sprite fullHealth;
    [SerializeField] Sprite missingHealth;
    [SerializeField] Sprite fullSpirit;
    [SerializeField] Sprite missingSpirit;

    GameController gc;
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Score
        scoreDisplay.text = ScoreTracker.GetScore().ToString();

        //Get Spirit
        if (gc.IsSpiritCooldown())
        {
            SpiritDisplay.sprite = missingSpirit;
        }
        else
        {
            SpiritDisplay.sprite = fullSpirit;
        }

        //Get Health
        int hp = pc.GetHealth();
        for (int i = 0; i < 5; i++)
        {
            if (hp > 0)
            {
                HealthCounter[i].sprite = fullHealth;
            }
            else
            {
                HealthCounter[i].sprite = missingHealth;
            }

            hp--;
        }
    }
}
