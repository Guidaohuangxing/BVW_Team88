using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : AttackObject
{
    public Player[] players;
    public int healAmount = 10;
    private void Start()
    {
        players = FindObjectsOfType<Player>();
    }

    public override void HitPlayer()
    {
        foreach(var item in players)
        {
            item.TakeHeal(healAmount);
        }
    }
}
