using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class SlashCollider : MonoBehaviour
{
    public Transform tipsTransform;
    public Transform baseTransform;
    [SerializeField]
    private float _forceAppliedToCut = 3f;
    private void OnTriggerEnter(Collider other)
    {
        Attackable attackable = other.gameObject.GetComponent<Attackable>();
        //print("cut things");
        if(attackable != null && other.GetComponent<AttackObject>().attackable)
        {
            other.GetComponent<AttackObject>().attackable = false;
            attackable.onHit();
            Vector3 tipsExitPosition = tipsTransform.position + new Vector3(0, 0, 2);
            //slice
            Vector3 side1 = tipsExitPosition - tipsTransform.position;
            Vector3 side2 = tipsExitPosition - baseTransform.position;

            Vector3 normal = Vector3.Cross(side1, side2).normalized;
            Vector3 transformedNormal = ((Vector3)(other.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;
            Vector3 transformedStartingPoint = other.gameObject.transform.InverseTransformPoint(tipsTransform.position);
            Plane plane = new Plane();

            plane.SetNormalAndPosition(
                    transformedNormal,
                    transformedStartingPoint);

            var direction = Vector3.Dot(Vector3.up, transformedNormal);
            if (direction < 0)
            {
                plane = plane.flipped;
            }
            GameObject[] slices = Slicer.Slice(plane, other.gameObject);
            Destroy(other.gameObject);

            Rigidbody rigidbody = slices[1].GetComponent<Rigidbody>();
            Vector3 newNormal = transformedNormal + Vector3.up * _forceAppliedToCut;
            rigidbody.AddForce(newNormal, ForceMode.Impulse);
        }

        

    }
}
