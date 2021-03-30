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
	Quaternion defaultRotationQuaternion;
	float speedСontroller;
	bool isMoveTodefaultposition;
	#endregion

	#region Methods
	// Use this for initialization
	void Start()
	{
		playerScript = player.GetComponent<Player>();
		mainCamera = GetComponent<Camera>();
		defaultRotationQuaternion = Quaternion.Euler(defaultRotation);

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
			speedСontroller = Vector3.Distance(transform.position, player.position + offset) + 10f;
			MoveToPosition(player.position + offset, speedСontroller);

			transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotationQuaternion, 15f / speedСontroller * Time.deltaTime);
			if (speedСontroller < 0.2f + 10f && Input.GetKeyDown(KeyCode.Space))
				StartGameplay();
		}
		else if (!Game.Started)  // Rotate around the base
			transform.RotateAround(rotationCenterOnStart, Vector3.up, rotationSpeed * Time.deltaTime);
		if (transform.rotation != defaultRotationQuaternion && Game.Started)
			transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotationQuaternion, 15f / speedСontroller * Time.deltaTime);
	}

	void StartGameplay()
	{
		player.GetComponent<PlayerOnBaseMovement>().StartTakeOffFromBase();
		canvasButtons.StartGameplay();
	}
	public void MoveToDefaultPosition() => isMoveTodefaultposition = true;
	void MoveToPosition(Vector3 target, float speedСontroller)
    {
		transform.position = Vector3.Lerp(transform.position, target, movementSpeed / speedСontroller * Time.deltaTime);
	}
	#endregion
}