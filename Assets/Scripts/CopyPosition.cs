using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] Transform target;
	[SerializeField] float speed;

	Vector3 offset;

	// Use this for initialization
	void Start()
	{
		offset = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (target)
		{
			transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offset), speed);
		}
	}
}