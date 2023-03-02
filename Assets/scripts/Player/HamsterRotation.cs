using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterRotation : MonoBehaviour
{
    void Awake()
    {
        transform.rotation = Quaternion.identity;
    }
    void LateUpdate()
    {
		Vector3 velocity = transform.parent.GetComponent<Rigidbody>().velocity;
        if (new Vector3(velocity.x, 0, velocity.z).magnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }

}
