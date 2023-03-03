using UnityEngine;
using YuetilitySoftbody;


public class SoftbodyRoll : MonoBehaviour
{
    public float JumpFactor = 50f;
    public float RollFactor = 50f;

    private Rigidbody rigid;
    private float counter = 1f;

    public bool stopInput = false;
    private Player player;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if (counter < 0)
            counter = 0;


        if (player.stopInput)
            return;


        if (Input.GetButton("Jump") && counter <= 0)
        {
            rigid.AddForce(Vector3.up * JumpFactor);
            counter = 1f;
        }



        // rotate based on camera
        var cam = Camera.main;
        var camForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        var camRight = cam.transform.right;

        var move = Input.GetAxisRaw("Vertical") * camRight + -Input.GetAxisRaw("Horizontal") * camForward;
        rigid.AddTorque(move * RollFactor);

        // add a little force also
        rigid.AddForce(move * RollFactor * 0.1f);

    }
}