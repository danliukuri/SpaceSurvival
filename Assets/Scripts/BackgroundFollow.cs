using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    #region Fields
    [SerializeField] float parralax;

	Material material;
	Vector2 offset;
    #endregion

    #region Methods
    private void Start()
    {
		material = GetComponent<MeshRenderer>().material;
		offset = material.mainTextureOffset;
	}
    void FixedUpdate ()
	{
		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.z / transform.localScale.y / parralax;

		material.mainTextureOffset = offset;
	}
    #endregion
}
