using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] Transform target;
	[SerializeField] float speed;

	Camera mainCamera;
	Vector3 mousePosition;
	Vector3 offset;
	float speedСontroller;

	// Use this for initialization
	void Start()
	{
		mainCamera = Camera.main;
		offset = transform.position;
		Cursor.lockState = CursorLockMode.Confined;

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(1) && target)
		{
			mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.y = 0f;
			speedСontroller = (mousePosition - transform.position).magnitude;
			transform.position = Vector3.Lerp(transform.position, mousePosition + offset, speed / speedСontroller * Time.deltaTime);
		}
	}
}