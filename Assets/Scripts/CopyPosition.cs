using UnityEngine;

public class CopyPosition : MonoBehaviour
{
	#region Fields
	[Header("Parameters")]
	[SerializeField] Transform player;
	[SerializeField] float speed;

	Player playerScript;
	Camera mainCamera;
	Vector3 mousePosition;
	Vector3 offset;
	float speedСontroller;
	#endregion

	#region Methods
	// Use this for initialization
	void Start()
	{
		playerScript = player.GetComponent<Player>();
		mainCamera = Camera.main;
		offset = transform.position;
		Cursor.lockState = CursorLockMode.Confined;
	}

    void FixedUpdate()
	{
		if (Input.GetMouseButton(1) && playerScript.IsActive && player)
		{
			mousePosition = Input.mousePosition;
			mousePosition.z = Vector3.Dot(player.position - transform.position, transform.forward);
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

			speedСontroller = (mousePosition - transform.position).magnitude;
			transform.position = Vector3.Lerp(transform.position, mousePosition + offset, speed / speedСontroller * Time.deltaTime);
		}
		else if (!playerScript.IsActive)
		{
			transform.position = Vector3.Lerp(transform.position, player.position + offset, speed / speedСontroller * Time.deltaTime);
		}
	}
    #endregion
}