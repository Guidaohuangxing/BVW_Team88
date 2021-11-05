using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject debugPrefab;

    public List<Vector3> pastPos = new List<Vector3>();
    public void SlashOrgin(Vector3 origin)
    {
        transform.position = origin;
    }
    public void UpadateSlash(Vector3 trackerPos, Vector3 offset)
    {
        Vector3 slashPos = trackerPos - offset;
        pastPos.Add(slashPos);
        if(pastPos.Count > 2)
        {
            if (debugPrefab)
            {
                Destroy(Instantiate(debugPrefab, slashPos, Quaternion.identity), .5f);
            }
            
            //BoxCollider bc = new GameObject("")
        }

    }
}
