using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject cam;
    private Vector3 OriginalPos, currentPos; 

    public void Start() {
        OriginalPos = cam.transform.position; 
    }

    public void resetCam() {
        cam.transform.position = OriginalPos;
    }

    public void TinyShake() { StartCoroutine(SmallShake()); }
    public void Shake() { StartCoroutine(MediumShake()); }
    public void BigShake() { StartCoroutine(HugeShake()); }

    IEnumerator SmallShake()
    {
        float lerpTime = 0.2f;
        float LUt,RDt,LDt,RUt, OGt =0f;

        Vector3 moveLeftUp, moveRightDown, moveLeftDown, moveRightUp;

        float left = -0.009f;
        float down = -0.009f;
        float right = 0.009f;
        float up = 0.009f;

        //set vectors
        moveLeftUp = new Vector3(OriginalPos.x + left, OriginalPos.y + up, OriginalPos.z);
        moveLeftDown = new Vector3(OriginalPos.x + left, OriginalPos.y + down, OriginalPos.z);
        moveRightUp = new Vector3(OriginalPos.x + right, OriginalPos.y + up, OriginalPos.z);
        moveRightDown = new Vector3(OriginalPos.x + right, OriginalPos.y + down, OriginalPos.z);

        int count = 0;
        int iterationTimes = 1;
        while(count < iterationTimes){
            LUt = 0f;
            RDt = 0f;
            LDt = 0f;
            RUt = 0f;
            OGt = 0f;
            //move up and left
            while (LUt< lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveLeftUp, (LUt / lerpTime));
                LUt += Time.deltaTime;
                yield return null;
            }
            //move down and right
            while (RDt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveRightDown, (RDt / lerpTime));
                RDt += Time.deltaTime;
                yield return null;
            } 
            //move down and left
            while (LDt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveLeftDown, (LDt / lerpTime));
                LDt += Time.deltaTime;
                yield return null;
            }
            //Move right and up
            while (RUt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveRightUp, (RUt / lerpTime));
                RUt += Time.deltaTime;
                yield return null;
            } 
            //Move back to position
            while (OGt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, OriginalPos, (OGt / lerpTime));
                OGt += Time.deltaTime;
                yield return null;
            }
            //yield return null;
            count = count + 1;
        }

        cam.transform.position = OriginalPos;
        yield return null;

    }

    IEnumerator MediumShake()
    {
        float lerpTime = 0.2f;
        float LUt, RDt, LDt, RUt, OGt = 0f;

        Vector3 moveLeftUp, moveRightDown, moveLeftDown, moveRightUp;

        float left = -0.019f;
        float down = -0.019f;
        float right = 0.019f;
        float up = 0.019f;

        //set vectors
        moveLeftUp = new Vector3(OriginalPos.x + left, OriginalPos.y + up, OriginalPos.z);
        moveLeftDown = new Vector3(OriginalPos.x + left, OriginalPos.y + down, OriginalPos.z);
        moveRightUp = new Vector3(OriginalPos.x + right, OriginalPos.y + up, OriginalPos.z);
        moveRightDown = new Vector3(OriginalPos.x + right, OriginalPos.y + down, OriginalPos.z);

        int count = 0;
        int iterationTimes = 1;
        while (count < iterationTimes)
        {
            LUt = 0f;
            RDt = 0f;
            LDt = 0f;
            RUt = 0f;
            OGt = 0f;
            //move up and left
            while (LUt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveLeftUp, (LUt / lerpTime));
                LUt += Time.deltaTime;
                yield return null;
            }
            //move down and right
            while (RDt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveRightDown, (RDt / lerpTime));
                RDt += Time.deltaTime;
                yield return null;
            }
            //move down and left
            while (LDt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveLeftDown, (LDt / lerpTime));
                LDt += Time.deltaTime;
                yield return null;
            }
            //Move right and up
            while (RUt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveRightUp, (RUt / lerpTime));
                RUt += Time.deltaTime;
                yield return null;
            }
            //Move back to position
            while (OGt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, OriginalPos, (OGt / lerpTime));
                OGt += Time.deltaTime;
                yield return null;
            }
            //yield return null;
            count = count + 1;
        }

        cam.transform.position = OriginalPos;
        yield return null;

    }

    IEnumerator HugeShake()
    {
        float lerpTime = 0.2f;
        float LUt, RDt, LDt, RUt, OGt = 0f;

        Vector3 moveLeftUp, moveRightDown, moveLeftDown, moveRightUp;

        float left = -0.03f;
        float down = -0.03f;
        float right = 0.03f;
        float up = 0.03f;

        //set vectors
        moveLeftUp = new Vector3(OriginalPos.x + left, OriginalPos.y + up, OriginalPos.z);
        moveLeftDown = new Vector3(OriginalPos.x + left, OriginalPos.y + down, OriginalPos.z);
        moveRightUp = new Vector3(OriginalPos.x + right, OriginalPos.y + up, OriginalPos.z);
        moveRightDown = new Vector3(OriginalPos.x + right, OriginalPos.y + down, OriginalPos.z);

        int count = 0;
        int iterationTimes = 1;
        while (count < iterationTimes)
        {
            LUt = 0f;
            RDt = 0f;
            LDt = 0f;
            RUt = 0f;
            OGt = 0f;
            //move up and left
            while (LUt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveLeftUp, (LUt / lerpTime));
                LUt += Time.deltaTime;
                yield return null;
            }
            //move down and right
            while (RDt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveRightDown, (RDt / lerpTime));
                RDt += Time.deltaTime;
                yield return null;
            }
            //move down and left
            while (LDt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveLeftDown, (LDt / lerpTime));
                LDt += Time.deltaTime;
                yield return null;
            }
            //Move right and up
            while (RUt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveRightUp, (RUt / lerpTime));
                RUt += Time.deltaTime;
                yield return null;
            }
            //Move back to position
            while (OGt < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, OriginalPos, (OGt / lerpTime));
                OGt += Time.deltaTime;
                yield return null;
            }
            //yield return null;
            count = count + 1;
        }

        cam.transform.position = OriginalPos;
        yield return null;

    }

}
