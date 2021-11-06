using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Attackable attackable = other.gameObject.GetComponent<Attackable>();
        print("cut things");
        if(attackable != null)
        {
            attackable.onHit();
        }
    }
}
