using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{

    public Material goalUnlit;
    public Material goalLit;

    private bool hasWon = false;

    private LevelManager levelManager;

    // Start is called before the first frame update

    void Start()
    {
        levelManager = GameObject.FindWithTag("GameController").GetComponent<LevelManager>();
    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasWon) {
            print("got to goal trigger");
            Win();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !hasWon) {
            print("got to goal collision");
            Win();
        }
    }

    private void Win() {
        hasWon = true;
        Material[] mats = GetComponent<Renderer>().materials;
        mats[1] = goalLit;
        GetComponent<Renderer>().materials = mats;
        levelManager.WinLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
