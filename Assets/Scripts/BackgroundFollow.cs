using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    #region Fields
    [SerializeField] float parralax;

	Material material;
	Vector2 offset;
    #endregion

    #region Methods
    void Start()
    {
		material = GetComponent<MeshRenderer>().material;
		offset = material.mainTextureOffset;
	}
	void FixedUpdate()
	{
		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.z / transform.localScale.y / parralax;

		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y);

		material.mainTextureOffset = offset;
	}
    #endregion
}
