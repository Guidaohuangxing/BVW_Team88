using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMisses : MonoBehaviour
{
    public SpawnBasedOnBPM spawnner;

    void OnTriggerEnter(Collider col) {
        //Debug.Log("Is colliding with: " + col.gameObject.tag);
        int missDeleted = -1;
        if (col.gameObject.tag == "TrashType1") 
        {
            missDeleted = spawnner.RemoveFromList("Type1");
        }
        else if(col.gameObject.tag == "TrashType2") 
        {
            missDeleted = spawnner.RemoveFromList("Type2");
        }
        else if (col.gameObject.tag == "TrashType3")
        {
            missDeleted = spawnner.RemoveFromList("Type3");
        }
        else if (col.gameObject.tag == "TrashType4")
        {
            missDeleted = spawnner.RemoveFromList("Type4");
        }
        else if (col.gameObject.tag == "TrashType4")
        {
            missDeleted = spawnner.RemoveFromList("Type4");
        }
        else if (col.gameObject.tag == "BossProjectile")
        {
            missDeleted = spawnner.RemoveFromList("Boss");
        }
        //Record a miss if something was actually deleted
        //if(missDeleted > 0)
        //{
        //    GameManager.Instance.RecordMiss();
        //}
    }
}
