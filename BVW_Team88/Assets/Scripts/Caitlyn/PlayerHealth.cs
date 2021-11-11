using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHp = 20;
    public Slider healthBar;
    public Text healthVal;

    public SoundFXManager sfx;


    public void TakeDamage(int dmg) {
        playerHp = playerHp - dmg;
        healthBar.value = playerHp;
        healthVal.text = playerHp + "/20";
        sfx.PlayHit();
    }

    public void Heal(int healPoints)
    {
        playerHp = playerHp + healPoints;
        healthBar.value = playerHp;
        healthVal.text = playerHp + "/20";

    }

    public bool CheckHealth() {
        if (playerHp <= 0)
        {
            return true;
        }
        else { return false; }
    }
}
