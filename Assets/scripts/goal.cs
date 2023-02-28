using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winScreen;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Finish")
        {
            winScreen.SetActive(true);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
