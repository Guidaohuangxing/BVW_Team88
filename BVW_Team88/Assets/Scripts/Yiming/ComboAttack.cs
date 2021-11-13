using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Player[] players;
    public List<Transform> swordTrackers = new List<Transform>();
    public AttackDecisionBar attackDecisionBar;//each time can attack then find this

    private void Start()
    {
        Instantiate();
    }

    private void Update()
    {
        
    }
    
    private void Instantiate()
    {
        players = GetComponentsInChildren<Player>();
    }



}
