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

    public Transform SwordTransform;
    private Vector3 swordSpriteTransformOffset;

    private Vector3 swordSpriteCurrentPos;//need a threshold to move record such pos
    private Vector3 swordSpritePreviousPos;
    public float SlashingThreshold = 0.5f;

    private void Start()
    {
        Initialized();
        
    }
    private void Update()
    {
        if(player.playerState == Player.State.Alive || player.playerState == Player.State.PowerUp)
        {
            RecordSwordPosition();
            speedTime += Time.deltaTime;
            if (speedTime >= speedTimer)
            {
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
                    UpdateSwordSpritePosition(currentPos, swordSpriteCurrentPos);
                    currentSlash.UpadateSlash(currentPos, slashOffset);
                    slashTimer = 0;
                }
                ParticleTrail.GetComponent<Rigidbody>().MovePosition(Vector3.ProjectOnPlane(currentPos, normalPlane) - slashOffset);
            }
        }
        

    }
    /// <summary>
    /// sprite position
    /// </summary>
    private void RecordSwordPosition()
    {
        if(Vector3.Distance(swordTracker.position, swordSpritePreviousPos) >= SlashingThreshold)
        {
            swordSpritePreviousPos = swordSpriteCurrentPos;
            swordSpriteCurrentPos = swordTracker.position;
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

        swordSpriteCurrentPos = swordTracker.position;
        swordSpritePreviousPos = swordSpriteCurrentPos;

    }
    public void StartSlash()
    {
        SwordTransform.position = player.SwordSpritePostion;
        swordSpriteTransformOffset = Vector3.ProjectOnPlane(swordSpriteCurrentPos, normalPlane) - player.SwordSpritePostion;
        UpdateSwordSpritePosition(currentPos, swordSpriteCurrentPos);
        //print("StartSlash");
        slashing = true;
        SlashPrefab.SetActive(true);
        currentSlash = SlashPrefab.GetComponent<Slash>();
        currentSlash.SlashOrgin(player.SwordStartPosition);
        slashOffset = Vector3.ProjectOnPlane(currentPos, normalPlane) - player.SwordStartPosition;
        
        currentSlash.UpadateSlash(currentPos, slashOffset);
        ParticleTrail.SetActive(true);
        //print(currentPos);
        //print(currentPos - slashOffset);
        ParticleTrail.transform.position = Vector3.ProjectOnPlane(currentPos, normalPlane) - slashOffset;
        
    }
    public void EndSlash()
    {
        SwordTransform.position = player.SwordSpritePostion;
        SwordTransform.rotation = Quaternion.identity;
        //print("StopSlash");
        slashing = false;
        currentSlash.Destory();
        slashTimer = 0;
        ParticleTrail.SetActive(false);
    }
 
    public void UpdateSwordSpritePosition(Vector3 SlashPoint, Vector3 TrackerPosition)
    {
        SwordTransform.position = Vector3.ProjectOnPlane(TrackerPosition, normalPlane) - swordSpriteTransformOffset;//first calculate position
        Vector3 slashp = Vector3.ProjectOnPlane(SlashPoint, normalPlane) - slashOffset;
        //work on rotation
        Vector3 dir = slashp - SwordTransform.position;
        float angle = Vector3.Angle(dir, SwordTransform.up);
        //print(angle);
        if(Vector3.Cross(dir, SwordTransform.up).z > 0)
        {
            angle = -angle;
        }
        Quaternion startRotate = SwordTransform.localRotation;
        Quaternion EndRoatate = Quaternion.AngleAxis(angle, Vector3.forward) * startRotate;
        SwordTransform.localRotation = Quaternion.Lerp(startRotate, EndRoatate,0.2f);

    }

}
