using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestart : MonoBehaviour
{
    
    public Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
         spawn = gameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < 70)
        {
            transform.position = spawn;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
