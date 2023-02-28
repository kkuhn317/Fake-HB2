using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalMaterialChange : MonoBehaviour
{
    public Material goalUnlit;
    public Material goalLit;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = goalUnlit;
    }

    // Update is called once per frame
    void Update()
    {
        if (winScreen.activeSelf == true)
        {
            GetComponent<Renderer>().material = goalLit;
        }
    }
}
