using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	// Start is called before the first frame update
    void Start()
    {
		findPlayer();

		// save the rotation
		camRotation = transform.rotation;
		newCamRotation = camRotation;

		// save the focal point
		focalPoint = objectToFollow.position + offset;

		// set the camera position
		transform.position = focalPoint + (transform.rotation * Vector3.forward * -distance);

    }

	void findPlayer() {
		objectToFollow = GameObject.FindWithTag("Player").transform;
	}


	private void MoveToTarget()
	{
		Vector3 _targetPos = objectToFollow.position + offset;
		//transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

		// move the focal point over time
		focalPoint = Vector3.Lerp(focalPoint, _targetPos, followSpeed * Time.deltaTime);
		// move the camera to the focal point
		transform.position = focalPoint + (transform.rotation * Vector3.forward * -distance);
	}

	private void RotateCamera() {
		// rotate the camera around the focal point to the new rotation with the given speed
		transform.rotation = Quaternion.Lerp(transform.rotation, newCamRotation, rotateSpeed * Time.deltaTime);

		// set the camera position to the focal point
		transform.position = focalPoint + (transform.rotation * Vector3.forward * -distance);

		
	}

	private void LateUpdate()
	{
		if (objectToFollow == null)
			findPlayer();

		if (!paused) {
			MoveToTarget();
			RotateCamera();
		}
	}

	public void SetNewRotate(Quaternion rotation, float speed) {
		newCamRotation = rotation;
		rotateSpeed = speed;
	}

	private Transform objectToFollow;

	private Vector3 focalPoint;

	public float distance = 0;

	public Vector3 offset = new Vector3(-7, 8, 7);
	public float followSpeed = 10;
	public bool paused = false;
	private Quaternion camRotation;
	public Quaternion newCamRotation;

	public float rotateSpeed = 0;
}