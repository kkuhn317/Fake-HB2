using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eightball : MonoBehaviour
{

    private AudioSource audioSource;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        eightBallHome = transform.position;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    public float movementX = 0;
    public float movementY = 0;

    private Vector3 eightBallHome;
    private Vector3 movement;

    private bool agroMode = false;


    // Update is called once per frame
    void Update()
    {
        targetPlayer();
        movement = new Vector3(movementX, 0, movementY);
        rb.AddForce(movement * 400);
    }

        public void targetPlayer()
    {
        movementX = 0;
        movementY = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 directionVector, normalizedVector;
            foreach (GameObject player in players) {
                //if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, eightBallHome) < 20) {
                if (Vector3.Distance(transform.position, player.transform.position) < 10 || agroMode) {
                    agroMode = true;
                    directionVector = player.transform.position - transform.position;
                    normalizedVector = new Vector3(directionVector.x, 0, directionVector.z).normalized;
                    movementX = normalizedVector.x;
                    movementY = normalizedVector.z;
                    return;
                }
            }

            // go back home
            // NEVER!!!
            //directionVector = eightBallHome - transform.position;
            //normalizedVector = new Vector3(directionVector.x, 0, directionVector.z).normalized;
            //movementX = normalizedVector.x;
            //movementY = normalizedVector.z;

    }

        Transform GetClosestEnemy(List<GameObject> enemies, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (GameObject potentialTargetObject in enemies)
        {
            Transform potentialTarget = potentialTargetObject.transform;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = dir.normalized;

            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir*800);
            GetComponent<Rigidbody>().AddForce(dir*-500);
            audioSource.Play();
        }
    }
}
