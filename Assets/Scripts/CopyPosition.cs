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
	Vector3 tmpDir;

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
			{
				tmpDir = mousePosition - transform.position;
				transform.position = Vector3.Lerp(transform.position, mousePosition + offset, speed * 1f / tmpDir.magnitude * Time.deltaTime);
			}
		}
	}
}