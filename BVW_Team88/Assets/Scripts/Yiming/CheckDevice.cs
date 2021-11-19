using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CheckDevice : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject Device1;
    // public GameObject Device2;
    public SteamVR_TrackedObject Device1;
    public SteamVR_TrackedObject Device2;
    void Start()
    {
        //Device1 = FindObjectsOfType<SteamVR_TrackedObject>()[0];
        //Device2 = FindObjectsOfType<SteamVR_TrackedObject>()[1];
        uint index1 = 0;
        uint index2 = 0;
        var error = ETrackedPropertyError.TrackedProp_Success;
        for (uint i = 0; i < 16; i++)
        {
            //Debug.Log(i);
            var result = new System.Text.StringBuilder((int)64);
            OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_RenderModelName_String, result, 64, ref error);
            Debug.Log(result.ToString());
            if (result.ToString().Contains("tracker"))
            {
                if (index1 == 0 && i != 0)
                {
                    index1 = i;
                }
                if (index1 != 0 && index1 != i)
                {
                    index2 = i;
                }
                //break;
            }
        }
        Device1.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)index1;
        Device2.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)index2;
    }
}
