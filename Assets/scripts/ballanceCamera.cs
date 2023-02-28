using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballanceCamera : MonoBehaviour {

	// Start is called before the first frame update
    void Start()
    {
		camRotationX = transform.rotation.eulerAngles.x;
		camRotationZ = transform.rotation.eulerAngles.z;

    }

	public void LookAtTarget()
	{
		calculateCameraRotation();
		Quaternion _rot = Quaternion.Euler(new Vector3(camRotationX, camRotationY, camRotationZ));
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	void lookOnly() {
        transform.LookAt(objectToFollow);
    }

	public void calculateCameraRotation() {
		
		if (!Input.GetKey(KeyCode.LeftShift))
			return;

		//print("got here");

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            camRotationY += 90;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            camRotationY -= 90;
        }

		while (camRotationY >= 360)
			camRotationY -= 360;

		while (camRotationY < 0)
			camRotationY += 360;

	}


	public void MoveToTarget()
	{
		//Vector3 _targetPos = objectToFollow.position + 
		//					 objectToFollow.forward * offset.z + 
		//					 objectToFollow.right * offset.x + 
		//					 objectToFollow.up * offset.y;
		
		Vector3 _targetPos = objectToFollow.position + calcOffset();
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}

	public Vector3 calcOffset() {
		if (!ballanceCamControls)
			return offset;

		switch (camRotationY) {
			case 0:
				return offset;
			case 90:
				return new Vector3(offset.z, offset.y, -offset.x);
			case 180:
				return new Vector3(-offset.x, offset.y, -offset.z);
			case 270:
				return new Vector3(-offset.z, offset.y, offset.x);
		}
		return offset;	// should never be reached
		

	}

	private void LateUpdate()
	{
		if (!paused) {
			if (lookOnlyMode) {
				lookOnly();
			} else {
				if (ballanceCamControls)
					LookAtTarget();
				MoveToTarget();
			}
		}
	}

	public Transform objectToFollow;

	public bool ballanceCamControls = false;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;
	public bool paused = false;
	public bool lookOnlyMode = false;
	private float camRotationX, camRotationZ, camRotationY;
}