using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // disable renderer
        //GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            savePlayer(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Player") {
            savePlayer(other.gameObject);
        }
    }

    public void savePlayer(GameObject player)
    {
        player.GetComponent<Player>().respawnPoint = player.transform.position;
    }
}
