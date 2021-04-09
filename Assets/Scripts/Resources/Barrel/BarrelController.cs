using UnityEngine;

namespace Resources.Barrel
{
    public class BarrelController : MonoBehaviour
    {
        #region Fields
        [SerializeField] float movementSpeed;
        [SerializeField] float rotationSpeed;
        Rigidbody rgdbody;
        #endregion

        #region Methods
        /// <summary> Random value from [-1.5; -0.5] or [0.5; 1.5] </summary>
        float MyRandomValue() => (Random.value + 0.5f) * Random.Range(-1, 2);

        // Start is called before the first frame update
        void Start()
        {
            rgdbody = GetComponent<Rigidbody>();
            rgdbody.velocity = new Vector3(MyRandomValue(), 0f, MyRandomValue()) * movementSpeed;
            rgdbody.angularVelocity = new Vector3(MyRandomValue(), MyRandomValue(), MyRandomValue()) * rotationSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {

        }
        #endregion
    }
}