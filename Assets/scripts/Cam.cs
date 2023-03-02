using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	// Start is called before the first frame update
    void Start()
    {
		camRotationX = transform.rotation.eulerAngles.x;
		camRotationZ = transform.rotation.eulerAngles.z;

    }

	void findPlayer() {
		objectToFollow = GameObject.FindWithTag("Player").transform;
	}

	void lookOnly() {
        transform.LookAt(objectToFollow);
    }


	public void MoveToTarget()
	{
		//Vector3 _targetPos = objectToFollow.position + 
		//					 objectToFollow.forward * offset.z + 
		//					 objectToFollow.right * offset.x + 
		//					 objectToFollow.up * offset.y;
		
		Vector3 _targetPos = objectToFollow.position + offset;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}

	private void LateUpdate()
	{
		if (objectToFollow == null)
			findPlayer();

		if (!paused) {
			if (lookOnlyMode) {
				lookOnly();
			} else {
				MoveToTarget();
			}
		}
	}

	private Transform objectToFollow;

	public Vector3 offset = new Vector3(-7, 8, 7);
	public float followSpeed = 10;
	public bool paused = false;
	public bool lookOnlyMode = false;
	private float camRotationX, camRotationZ, camRotationY;
}