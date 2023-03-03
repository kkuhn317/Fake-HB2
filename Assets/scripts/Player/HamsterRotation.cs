using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterRotation : MonoBehaviour
{
    private Player player;


    void Awake()
    {
        transform.rotation = Quaternion.identity;
    }

    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    void LateUpdate()
    {
        if (player.isRespawning) {
            transform.rotation = Quaternion.identity;
            return;
        }


		Vector3 velocity = transform.parent.GetComponent<Rigidbody>().velocity;
        if (new Vector3(velocity.x, 0, velocity.z).magnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
        }
    }




}
