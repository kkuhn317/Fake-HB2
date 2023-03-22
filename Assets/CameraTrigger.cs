using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Quaternion newCamRotation;
    public float rotateSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // disable renderer
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player enters the trigger
        if (other.tag == "Player") {
            var camera = Camera.main;
            var cam = camera.GetComponent<Cam>();
            cam.SetNewRotate(newCamRotation, rotateSpeed);
        }
    }
}
