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

    public float speed = 0;

    public float speedTimer = 0.05f;
    private float speedTime = 0;

    public GameObject ParticleTrail;
    private void Start()
    {
        Initialized();
    }
    private void Update()
    {
       
        speedTime += Time.deltaTime;
        if(speedTime >= speedTimer)
        {
            //currentPos = swordTracker.InverseTransformPoint(swordTracker.transform.position + swordTracker.transform.forward * swordLength);
            currentPos = swordTracker.transform.position + swordTracker.transform.forward * swordLength;
            speed = Mathf.Clamp((currentPos - previousPos).magnitude / speedTime, 0, 300);
            if (speed > minSpeed && !slashing)
            {
                StartSlash();
            }
            else if (speed <= minSpeed && slashing)
            {
                EndSlash();
            }
            previousPos = currentPos;
            speedTime = 0f;
            //print(slashing);
           
        }
        if (slashing)
        {
            
            slashTimer += Time.deltaTime;
            if (slashTimer > (1 / UpdateInterval))
            {
                //currentSlash.UpadateSlash(transform.TransformPoint(currentPos), slashOffset);
                currentSlash.UpadateSlash(currentPos, slashOffset);
                slashTimer = 0;
            }
            ParticleTrail.GetComponent<Rigidbody>().MovePosition(Vector3.ProjectOnPlane(currentPos, normalPlane) - slashOffset);
        }





    }


    private void Initialized()
    {
        // currentPos = swordTracker.InverseTransformPoint(swordTracker.transform.position + swordTracker.transform.forward * swordLength);
        currentPos = swordTracker.transform.position + swordTracker.transform.forward * swordLength;
        previousPos = currentPos;
        gameManager = FindObjectOfType<GameManager>();
        player = GetComponentInParent<Player>();
        ParticleTrail.SetActive(false);
        
      
    }
    public void StartSlash()
    {
        //print("StartSlash");
        slashing = true;
        SlashPrefab.SetActive(true);
        currentSlash = SlashPrefab.GetComponent<Slash>();
        currentSlash.SlashOrgin(player.SwordStartPosition);
        slashOffset = Vector3.ProjectOnPlane(currentPos, normalPlane) - player.SwordStartPosition;
        
        currentSlash.UpadateSlash(currentPos, slashOffset);
        ParticleTrail.SetActive(true);
        print(currentPos);
        print(currentPos - slashOffset);
        ParticleTrail.transform.position = Vector3.ProjectOnPlane(currentPos, normalPlane) - slashOffset;
        
    }
    public void EndSlash()
    {
        print("StopSlash");
        slashing = false;
        currentSlash.Destory();
        slashTimer = 0;
        ParticleTrail.SetActive(false);
    }
    
}
