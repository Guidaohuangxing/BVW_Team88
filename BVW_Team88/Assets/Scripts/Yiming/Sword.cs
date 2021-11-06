using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //turn the vector3 from vive tracker to paths
    //vive tracker movement
    public Transform swordTracker;
    //sword position on which path
    public int position = 0;

    public float swordLength = 5f;
    public Vector3 currentPos;


    public Vector3 previousPos;
    //minmun speed to make slash;
    public float minSpeed = 30f;

    public bool slashing = false;

    public GameManager gameManager;

    public Vector3 slashOffset;

    private Slash currentSlash;
    private float slashTimer = 0;
    public int UpdateInterval = 50;
    public GameObject SlashPrefab;
    public Player player;

    Vector3 normalPlane = new Vector3(0, 0, 1);
    

    
    
    private void Start()
    {
        Initialized();
    }
    private void Update()
    {
        //currentPos = swordTracker.InverseTransformPoint(transform.position + transform.forward * swordLength);
        currentPos = swordTracker.transform.position + transform.forward * swordLength;
        //print((currentPos - previousPos).magnitude / Time.deltaTime);
        if ((currentPos - previousPos).magnitude / Time.deltaTime > minSpeed && !slashing)
        {
            StartSlash();
        }
        else if((currentPos - previousPos).magnitude / Time.deltaTime <= minSpeed && slashing)
        {
            EndSlash();
        }
        

        if (slashing)
        {
            slashTimer += Time.deltaTime;
            if(slashTimer >(1/ UpdateInterval))
            {
                currentSlash.UpadateSlash(currentPos, slashOffset);
                slashTimer = 0;
            }
        }
        previousPos = currentPos;
    }


    private void Initialized()
    {
        // currentPos = swordTracker.InverseTransformPoint(transform.position + transform.forward * swordLength);
        currentPos = swordTracker.transform.position + transform.forward * swordLength;
        previousPos = currentPos;
        gameManager = FindObjectOfType<GameManager>();
        player = GetComponentInParent<Player>();
      
    }
    public void StartSlash()
    {
        print("StartSlash");
        slashing = true;
        SlashPrefab.SetActive(true);
        currentSlash = SlashPrefab.GetComponent<Slash>();
        currentSlash.SlashOrgin(player.SwordStartPosition);
        slashOffset = Vector3.ProjectOnPlane(currentPos, normalPlane) - player.SwordStartPosition;
        currentSlash.UpadateSlash(currentPos, slashOffset);
    }
    public void EndSlash()
    {
        print("StopSlash");
        slashing = false;
        currentSlash.Destory();
    }
    
}
