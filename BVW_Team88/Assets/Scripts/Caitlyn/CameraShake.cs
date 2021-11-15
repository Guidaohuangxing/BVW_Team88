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
        StartCoroutine(ResetCamera());
    }
    public void TinyShake() { StartCoroutine(SmallShake()); }
    public void Shake() { StartCoroutine(MediumShake()); }
    public void BigShake() { StartCoroutine(HugeShake()); }
    public void ShakeBackwards() { StartCoroutine(ShakeBack()); }

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

    IEnumerator ShakeBack() {
        float lerpTime = 0.2f;
        float b, f, OGt = 0f;

        Vector3 moveBack, moveForward;

        float forward = 0.1f;
        float back = -0.2f;

        //set vectors
        moveBack= new Vector3(OriginalPos.x, OriginalPos.y, OriginalPos.z+back);
        moveForward= new Vector3(OriginalPos.x, OriginalPos.y, OriginalPos.z+forward);

        int count = 0;
        int iterationTimes = 1;

        //shake
        while (count < iterationTimes)
        {
            b = 0f;
            f = 0f;
            OGt = 0f;
           
            //move back a little
            while (b < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveBack, (b / lerpTime));
                b += Time.deltaTime;
                yield return null;
            }
            //move forward a little
            while (f < lerpTime)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, moveForward, (f / lerpTime));
                f += Time.deltaTime;
                yield return null;
            }
            //Move back to back position
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

    IEnumerator ResetCamera() {
        float lerpTime = 0.2f;
        float time = 0;
        while (time < lerpTime)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, OriginalPos, (time / lerpTime));
            time += Time.deltaTime;
            yield return null;
        }
        cam.transform.position = OriginalPos;
        yield return null;
    }
}
