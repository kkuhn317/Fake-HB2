using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool stopInput = false;

    public bool isRespawning = false;

    public Vector3 respawnPoint;
    public GameObject glow;
    private Rigidbody rb;

    private LevelManager levelManager;

    private Quaternion startRotation;


    public void respawn()
    {

        // set the player's position to the respawn point
        transform.position = respawnPoint;

        if (rb) {
            // reset the velocity
            rb.velocity = Vector3.zero;
            // reset the angular velocity
            rb.angularVelocity = Vector3.zero;
        }

        // reset the rotation
        transform.rotation = startRotation;

        // turn on the glow
        if (glow)
            glow.SetActive(true);

        // turn off the player's input
        stopInput = true;

        isRespawning = true;

        // turn on player input after 1 second
        Invoke("enableInput", 1f);
    }

    public void enableInput()
    {
        stopInput = false;
        isRespawning = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        levelManager = GameObject.FindWithTag("GameController").GetComponent<LevelManager>();
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.0001f && !stopInput)
        {
            // remove the glow
            if (glow)
                glow.SetActive(false);
        }


        // check if player falls off the map
        if (transform.position.y < levelManager.deathHeight)
        {
            respawn();
        }


    }

    
}
