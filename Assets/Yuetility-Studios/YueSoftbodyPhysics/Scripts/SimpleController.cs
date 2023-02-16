using UnityEngine;
using YuetilitySoftbody;

namespace YuetilitySoftbody
{
    public class SimpleController : MonoBehaviour
    {
        public float JumpFactor = 50f;
        public float RollFactor = 50f;

        private Rigidbody rigid;
        private float counter = 1f;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        void Update()
        {
            counter -= Time.deltaTime;

            if (counter < 0)
                counter = 0;

            if (Input.GetButton("Jump") && counter <= 0)
            {
                rigid.AddForce(Vector3.up * JumpFactor);
                counter = 1f;
            }

            //rigid.AddTorque(Vector3.forward * RollFactor * Input.GetAxis("Horizontal"));
            //rigid.AddTorque(Vector3.right * RollFactor * -Input.GetAxis("Vertical"));

            // instead rotate based on camera
            var cam = Camera.main;
            var camForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
            var camRight = cam.transform.right;

            var move = Input.GetAxis("Vertical") * camRight + -Input.GetAxis("Horizontal") * camForward;
            rigid.AddTorque(move * RollFactor);

            // add a little force also
            rigid.AddForce(move * RollFactor * 0.1f);

        }
    }
}