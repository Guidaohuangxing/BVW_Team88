using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject debugPrefab;
    public int newestSlashColliderNumber = 0;// a pointer follow the order in the pool
    public Queue<Vector3> pastPos  = new Queue<Vector3>();
    public int limitPos = 10;
    public List<GameObject> SlashColliders;
    public void SlashOrgin(Vector3 origin)
    {
        transform.position = origin;
        pastPos.Clear();
        foreach(var item in SlashColliders)
        {
            item.SetActive(false);
        }
    }
    public void UpadateSlash(Vector3 trackerPos, Vector3 offset)
    {
        trackerPos = Vector3.ProjectOnPlane(trackerPos, new Vector3(0, 0, 1));
        Vector3 slashPos = trackerPos - offset;
        //print(slashPos);
        if(pastPos.Count < limitPos)
        {
            pastPos.Enqueue(slashPos);
            Vector3[] poss = pastPos.ToArray();
            if(poss.Length >= 2)
            {
                SlashColliders[newestSlashColliderNumber].SetActive(true);
                GameObject go = SlashColliders[newestSlashColliderNumber];
                go.transform.position = (poss[poss.Length - 1] + poss[poss.Length - 2]) / 2;
                Vector3 dir = poss[poss.Length - 1] - poss[poss.Length - 2];
                Quaternion rotateTo = Quaternion.FromToRotation(go.transform.up, dir);
                //print(rotateTo);
                go.transform.rotation = rotateTo * go.transform.rotation;
                newestSlashColliderNumber++;
            }
        }
        else if(pastPos.Count == limitPos)
        {
            pastPos.Dequeue();
            
            pastPos.Enqueue(slashPos);
            Vector3[] poss = pastPos.ToArray();
            SlashColliders[newestSlashColliderNumber].SetActive(true);
            GameObject go = SlashColliders[newestSlashColliderNumber];
            go.transform.position = (poss[poss.Length - 1] + poss[poss.Length - 2]) / 2;
            Vector3 dir = poss[poss.Length - 1] - poss[poss.Length - 2];
            Quaternion rotateTo = Quaternion.FromToRotation(go.transform.up, dir);
            go.transform.rotation = rotateTo * go.transform.rotation;

            newestSlashColliderNumber++;
        }
        if(newestSlashColliderNumber == SlashColliders.Count)
        {
            newestSlashColliderNumber = 0;
        }

    }
    public void Destory()
    {
        
        foreach (var item in SlashColliders)
        {
            item.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    
}
