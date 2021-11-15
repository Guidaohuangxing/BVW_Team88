using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTips : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Attackable attackable = other.gameObject.GetComponent<Attackable>();
        if(attackable != null)
        {
            Material[] ms = other.GetComponent<Renderer>().materials;
            foreach(var item in ms)
            {
                item.SetFloat("_EdgePower", 0.1f);
            }
            print("set edge");
        }  
    }
}
