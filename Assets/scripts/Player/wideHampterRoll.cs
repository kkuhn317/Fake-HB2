using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wideHampterRoll : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;

    public bool stopInput = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (stopInput) {
            return;
        }
        
        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();


        float movementX = Input.GetAxisRaw("Horizontal");
		float movementY = Input.GetAxisRaw("Vertical");

        var movement = -forward * movementX + right * movementY;
            if (movement.magnitude > 1)
                movement.Normalize();

        rb.AddTorque(movement * speed, ForceMode.VelocityChange);
        
    }
}
