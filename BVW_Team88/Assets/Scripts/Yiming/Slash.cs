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
    public float NewColliderTheshold = 0.1f;
    private void Start()
    {
        foreach (var item in SlashColliders)
        {
            item.SetActive(false);
        }
    }
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
        }
        else if(pastPos.Count == limitPos)
        {
            pastPos.Dequeue();
            pastPos.Enqueue(slashPos);  
        }
        Vector3[] poss = pastPos.ToArray();
        if(Vector3.Distance(poss[poss.Length-1],poss[0])> NewColliderTheshold)
        {
            newestSlashColliderNumber = ActivateSlashCollider(poss, newestSlashColliderNumber);
            pastPos.Clear();
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



    /// <summary>
    /// use a pooling of collider to cut things
    /// </summary>
    /// <param name="previousPosition"></param>
    public int ActivateSlashCollider(Vector3[] previousPosition,int newestSlashColliderPointer)
    {
        
        if (previousPosition.Length >= 2)
        {
            SlashColliders[newestSlashColliderPointer].SetActive(true);
            GameObject go = SlashColliders[newestSlashColliderPointer];
            go.transform.position = (previousPosition[previousPosition.Length - 1] + previousPosition[0]) / 2;
            Vector3 dir = previousPosition[previousPosition.Length - 1] - previousPosition[0];
            Quaternion rotateTo = Quaternion.FromToRotation(go.transform.up, dir);
            //print(rotateTo);
            go.transform.rotation = rotateTo * go.transform.rotation;
            newestSlashColliderPointer++;
        }
        if (newestSlashColliderPointer == SlashColliders.Count)
        {
            newestSlashColliderPointer = 0;
        }
        return newestSlashColliderPointer;
    }
}
