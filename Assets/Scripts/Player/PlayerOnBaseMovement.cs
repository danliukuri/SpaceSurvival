using UnityEngine;

public class PlayerOnBaseMovement : MonoBehaviour
{
    #region Fields
    PlayerController playerController;

    Vector3 randomVector;
    Vector3 playerOnBasePosition;
    Vector3 targetPosition;

    bool isLandsOnTheBase;
    bool isTakeOffFromBase;
    bool isPlayerOnTheBase;

    float distanse;
    float playerSpeedController;
    float playerDefaultYPosition;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        playerDefaultYPosition = transform.position.y;
        playerOnBasePosition = new Vector3(0f, -2.9f, 0f);
        targetPosition = new Vector3();
        randomVector = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerOnTheBase && Input.GetKeyDown(KeyCode.Space) && IsPlayerOnTheBasePosition())
            StartLandsOnTheBase();
        else if (isPlayerOnTheBase && Input.GetKeyDown(KeyCode.Space))
            StartTakeOffFromBase();
    }
    private void FixedUpdate()
    {
        if (isLandsOnTheBase)
            LandsOnTheBaseUpdate();
        else if (isTakeOffFromBase)
            TakeOffFromBaseUpdate();
    }

    void StartLandsOnTheBase()
    {
        isLandsOnTheBase = true;
        playerController.IsActive = false;
    }
    void LandsOnTheBaseUpdate()
    {
        distanse = Vector3.Distance(transform.position, playerOnBasePosition);
        playerSpeedController = distanse < 1f ? 1f : distanse;

        transform.position = Vector3.MoveTowards(transform.position, playerOnBasePosition, playerSpeedController * 2f * Time.deltaTime);
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation,
            Quaternion.Euler(0f, 90f, 0f), (playerSpeedController + 2f) / playerSpeedController * Time.deltaTime);

        if (transform.position.y <= playerOnBasePosition.y)
        {
            isPlayerOnTheBase = true;
            isLandsOnTheBase = false;
        }
    }

    void StartTakeOffFromBase()
    {
        isTakeOffFromBase = true;
        isPlayerOnTheBase = false;
        randomVector.Set(Random.Range(-2f, 2f), Random.Range(-180f, 180f), Random.Range(-2f, 2f));
        targetPosition.Set(randomVector.x, playerDefaultYPosition, randomVector.z);
    }
    void TakeOffFromBaseUpdate()
    {
        distanse = Vector3.Distance(transform.position, targetPosition);
        playerSpeedController = distanse < 1f ? 1f : distanse;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 4f / playerSpeedController * Time.deltaTime);
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation,
            Quaternion.Euler(0f, randomVector.y, 0f), playerSpeedController / (playerSpeedController - 0.5f) * Time.deltaTime);

        if (transform.position.y >= playerDefaultYPosition)
        {
            isPlayerOnTheBase = false;
            isTakeOffFromBase = false;
            playerController.IsActive = true;
        }
    }

    bool IsPlayerOnTheBasePosition() => Mathf.Abs(transform.position.x) < 3f && Mathf.Abs(transform.position.z) < 3f;
    #endregion
}