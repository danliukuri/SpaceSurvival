using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	#region Fields
	[Header("Parameters")]
	[SerializeField] CanvasButtons canvasButtons;
	[SerializeField] Transform player;
	[SerializeField] float movementSpeed;
	[SerializeField] float rotationSpeed;
	[SerializeField] Vector3 offset;
	[SerializeField] Vector3 defaultRotation;
	[SerializeField] Vector3 rotationCenterOnStart;

	Player playerScript;
	Camera mainCamera;
	Vector3 mousePosition;
	float speedСontroller;
	bool isMoveTodefaultposition;
	#endregion

	#region Methods
	// Use this for initialization
	void Start()
	{
		playerScript = player.GetComponent<Player>();
		mainCamera = GetComponent<Camera>();

		Cursor.lockState = CursorLockMode.Confined;
	}
    void FixedUpdate()
	{
		if (Input.GetMouseButton(1) && playerScript.IsActive && Game.Started) // Copy mouse position
		{
			mousePosition = Input.mousePosition;
			mousePosition.z = Vector3.Dot(player.position - transform.position, transform.forward);
			mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

			speedСontroller = Vector3.Distance(mousePosition, transform.position);
			MoveToPosition(mousePosition + offset, speedСontroller);
		}
		else if (!playerScript.IsActive && Game.Started) // Copy player position
		{
			speedСontroller = Vector3.Distance(transform.position, player.position + offset) + 10f;
			MoveToPosition(player.position + offset, speedСontroller);
		}
		if (isMoveTodefaultposition && !Game.Started) // Move to player position
		{
			speedСontroller = Vector3.Distance(transform.position, player.position + offset) + movementSpeed - 1f;
			MoveToPosition(player.position + offset, speedСontroller);

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(defaultRotation), movementSpeed / speedСontroller * Time.deltaTime);

			if (speedСontroller < 0.2f + movementSpeed - 1f)
				canvasButtons.StartGameplay();
		}
		else if (!Game.Started)  // Rotate around the base
			transform.RotateAround(rotationCenterOnStart, Vector3.up, rotationSpeed * Time.deltaTime);
	}

    public void MoveToDefaultPosition() => isMoveTodefaultposition = true;
	void MoveToPosition(Vector3 target, float speedСontroller)
    {
		transform.position = Vector3.Lerp(transform.position, target, movementSpeed / speedСontroller * Time.deltaTime);
	}
	#endregion
}