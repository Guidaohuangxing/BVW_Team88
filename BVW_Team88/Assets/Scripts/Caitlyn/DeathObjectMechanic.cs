using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObjectMechanic : MonoBehaviour
{
    Vector3 goToLoc = new Vector3(1.06f, 4.34f, -28.92f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, goToLoc, 0.3f * Time.deltaTime);
    }

}
