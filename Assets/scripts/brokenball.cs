using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenball : MonoBehaviour
{
    public GameObject hb;

    // Start is called before the first frame update
    void Start()
    {
        // loop through all the children of the game object
        foreach (Transform child in transform) {
            // give child random upward velocity and random rotation
            child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5f, 5f), Random.Range(2f, 4f), Random.Range(-5f, 5f)), ForceMode.Impulse);
            child.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), ForceMode.Impulse);
        }
        
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
