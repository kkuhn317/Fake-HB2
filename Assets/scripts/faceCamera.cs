using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour
{

    private Transform cam;

    public float pulseRate = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // make the game object face the camera
        transform.LookAt(transform.position + cam.forward);

        if (pulseRate > 0) {

            // constantly make the gameobject pulse in transparency
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * pulseRate, 1f));
        }
    }

    public void turnoff()
    {
        // turn off the game object
        gameObject.SetActive(false);
    }
}
