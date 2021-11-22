using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDamage : MonoBehaviour
{

    Player player;
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        AttackObject ao = other.GetComponent<AttackObject>();
        if (ao != null && ao.canCauseDamage)
        {
            ao.HitPlayer();
            //print("do this in healing?");
            player.TakeDamage(ao.damage);
            ao.canCauseDamage = false;
            Destroy(ao.gameObject, .5f);
            SoundFXManager.instance.PlayHit();
        }
        else if(ao!= null && !ao.canCauseDamage)
        {
            ao.HitPlayer();
            Destroy(ao.gameObject, .5f);
        }
    }
}
