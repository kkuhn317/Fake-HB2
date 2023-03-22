using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the burger around the y axis
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    // when player touches the burger
    private void OnTriggerEnter(Collider other)
    {
        // if the player touches the burger
        if (other.gameObject.tag == "Player")
        {
            // get roll script
            Roll rollScript = other.gameObject.GetComponent<Roll>();
            rollScript.eatBurger();

            // make chomp sound
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            // make particle effect
            ParticleSystem particle = GetComponent<ParticleSystem>();
            particle.Play();

            // disable child object
            Transform child = transform.GetChild(0);
            child.gameObject.SetActive(false);
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;

            // destroy the burger after 1 second
            Destroy(gameObject, 1);
        }
    }
}
