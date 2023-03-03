using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Roll : MonoBehaviour
{
    public float speed = 0;
    public float airSpeed = 0;
    public float antiForce = 0;
    public float maxVelocity = 10;
    // A cached copy of the squared max velocity. Used in FixedUpdate.
    private float sqrMaxVelocity;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // used for mouse movement
    //private float mouseMovementX;
    //private float mouseMovementY;

    public bool grounded = false;
    public ArrayList hitPoints = new ArrayList();
    
    public bool overmaxSpeed = false;
    public float breakForce = -1f;
    public bool noBreakMode = false;    // overrides breakForce, used for powerups and hamsterball
    private bool inSafeZone = false;
    public bool hamsterballMode = false;
    public Vector3 forceapplied;
    public bool eightBallMode = false;
    private Vector3 eightBallHome;

    [Header("Sound Effects")]
    public AudioClip RollSound;
    public AudioClip hitBallSound;
    public float volumeDivider;
    public float pitchDivider;
    private AudioSource audioSource;

    [Header("Animation")]
    private Animator animator;

    public GameObject brokenBall;

    [Header("Checkpoints")]


    public GameObject hamster;

    private Player player;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        if (eightBallMode) {
            eightBallHome = transform.position;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = hitBallSound;
        }

        if (GetComponent<AudioSource>() && !eightBallMode) {
            audioSource = GetComponent<AudioSource>();

            // Note: this should be changed when specific rolling sounds are implemented
            audioSource.clip = RollSound;
            audioSource.volume = 0;
            audioSource.Play();
        }

        animator = GetComponent<Animator>();

        player = GetComponent<Player>();
        
    }

    public void GetInput()
	{
        // you can manually stop input
        if (player.stopInput) {
            movementX = 0;
            movementY = 0;
            return;
        }

		movementX = Input.GetAxisRaw("Horizontal");
		movementY = Input.GetAxisRaw("Vertical");

        //float sensitivity = PlayerPrefs.GetFloat("Sensitivity", .5f) * 4; 
        // TODO: Mouse movement?
        //float sensitivity = .5f * 4;

        //movementX += mouseMovementX * sensitivity;
        //movementY += mouseMovementY * sensitivity;

        //print("x is" + movementX);
        //print("y is" + movementY);
	}


    public void targetPlayer()
    {
        movementX = 0;
        movementY = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] fakePlayers = GameObject.FindGameObjectsWithTag("fakePlayer");
        Vector3 directionVector, normalizedVector;
            foreach (GameObject player in players) {
                if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, eightBallHome) < 20) {
                    directionVector = player.transform.position - transform.position;
                    normalizedVector = new Vector3(directionVector.x, 0, directionVector.z).normalized;
                    movementX = normalizedVector.x;
                    movementY = normalizedVector.z;
                    return;
                } else {
                    // if the player is too far away, move the 8ball to the closest fake player
                    Transform fakePlayerTarget = GetClosestEnemy(fakePlayers.ToList<GameObject>(), transform);
                        if (!fakePlayerTarget)
                            return;
                            
                        if (Vector3.Distance(transform.position, fakePlayerTarget.transform.position) < 5 && Vector3.Distance(transform.position, eightBallHome) < 20) {
                            directionVector = fakePlayerTarget.transform.position - transform.position;
                            normalizedVector = new Vector3(directionVector.x, 0, directionVector.z).normalized;
                            movementX = normalizedVector.x;
                            movementY = normalizedVector.z;
                            return;
                        }
                    
                }
            }

            // go back home
            directionVector = eightBallHome - transform.position;
            normalizedVector = new Vector3(directionVector.x, 0, directionVector.z).normalized;
            movementX = normalizedVector.x;
            movementY = normalizedVector.z;

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

    void OnCollisionEnter(Collision other) {
         
        if (other.gameObject.tag == "instaKill") {
            breakBall();
        }

        //if (eightBallMode && (other.gameObject.tag == "Player" || other.gameObject.tag == "8Ball")) {
        if (eightBallMode && (other.gameObject.GetComponent<Rigidbody>())) {

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

        // Calculate whether the ball should break from hitting the ground too hard
        float collisionForce = other.impulse.magnitude / Time.fixedDeltaTime;
        if (collisionForce > breakForce && breakForce > 0 && !inSafeZone && !noBreakMode) {
            breakBall();
        }

        OnCollisionStay(other);
    }

    public void breakBall() {
        Instantiate(brokenBall, transform.position, transform.rotation);

        player.respawn();
        
    }



    private void OnTriggerEnter(Collider other) {
        if (other.tag == "safeZone")
            inSafeZone = true;
        if (other.tag == "instaKill")
            reloadScene();
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "safeZone")
            inSafeZone = false;
    }

    void reloadScene() {
        if (eightBallMode || gameObject.tag == "fakePlayer") {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 1;
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    void OnCollisionStay(Collision collisionInfo) {
        
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            hitPoints.Add(contact.normal);
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    private void Update() {
        // increase the mouse movement variables every frame until it is called by fixedUpdate
        // this is so the movements are smooth
        //mouseMovementX += Input.GetAxisRaw("Mouse X");
        //mouseMovementY += Input.GetAxisRaw("Mouse Y");


        animator.speed = rb.velocity.magnitude / 10;
    }

    //private void resetMouseMovement() {
    //    mouseMovementX = 0;
    //    mouseMovementY = 0;
    //}


	private void FixedUpdate()
	{
        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        if (hitPoints.Count > 0)
            grounded = true;
        else
            grounded = false;

        Vector3 movement;

        if (!eightBallMode) {
            GetInput();
            //resetMouseMovement();
            //this is the direction in the world space we want to move:
            movement = forward * movementY + right * movementX;

            if (movement.magnitude > 1)
                movement.Normalize();
        } else {
            targetPlayer();
            movement = new Vector3(movementX, 0, movementY);
        }

        forceapplied = movement;

        if (grounded)
            rb.AddForce(movement * speed);
        else
            rb.AddForce(movement * airSpeed);
        


        // stop the hamsterball from going too fast
        if (Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.z * rb.velocity.z)) >= maxVelocity) {
            overmaxSpeed = true;
            //(whatever code you need to add force) in direction of arctan(Vy/Vx)
            rb.AddForce(new Vector3(-rb.velocity.x, 0, -rb.velocity.z).normalized * antiForce);
        }
        else {
            overmaxSpeed = false;
        }


        // Rolling Sounds!!!
        if (GetComponent<AudioSource>()) {
            if (!grounded) {
                audioSource.volume = 0;
            } else {
                audioSource.volume = rb.velocity.magnitude / volumeDivider;

                if (pitchDivider > 0) {
                    audioSource.pitch = rb.velocity.magnitude / pitchDivider;
                }
            }
        }

        hitPoints.Clear();
        
	}



}
