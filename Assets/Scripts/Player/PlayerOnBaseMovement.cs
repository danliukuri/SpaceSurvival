using UnityEngine;

public class PlayerOnBaseMovement : MonoBehaviour
{
    #region Fields
    [Header("Keys")]
    [SerializeField] KeyCode keyToUnloadResources;
    [SerializeField] KeyCode keyToTakeOffFromBase;
    [SerializeField] KeyCode keyToLandsOnTheBase;
    [Header("Base position")]
    [SerializeField] Vector3 playerOnBasePosition;
    [SerializeField] float basePositionRadius;

    Player playerScript;
    ParticlesController playerParticlesController;
    Timer timer = new Timer();

    Vector3 randomVector = new Vector3();
    Vector3 targetPosition = new Vector3();

    bool isLandsOnTheBase;
    bool isTakeOffFromBase;
    bool isPlayerOnTheBase;
    bool areResourcesBeingUnloaded;

    float distanse;
    float playerSpeedController;
    float playerDefaultYPosition;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<Player>();
        playerParticlesController = GetComponent<ParticlesController>();
        playerDefaultYPosition = transform.position.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isPlayerOnTheBase && Input.GetKeyDown(keyToLandsOnTheBase) && IsPlayerOnTheBasePosition())
            StartLandsOnTheBase();
        else if (!areResourcesBeingUnloaded && isPlayerOnTheBase)
            UnloadResources();
        else if (areResourcesBeingUnloaded && Input.GetKeyDown(keyToTakeOffFromBase))
        {
            areResourcesBeingUnloaded = false;
            StartTakeOffFromBase();
        }
    }
    void FixedUpdate()
    {
        if (isLandsOnTheBase)
            LandsOnTheBaseUpdate();
        else if (isTakeOffFromBase)
            TakeOffFromBaseUpdate();
    }

    void StartLandsOnTheBase()
    {
        isLandsOnTheBase = true;
        playerScript.IsActive = false;
    }
    void LandsOnTheBaseUpdate()
    {
        distanse = Vector3.Distance(transform.position, playerOnBasePosition);
        playerSpeedController = distanse < 1f ? 1f : distanse;

        transform.position = Vector3.MoveTowards(transform.position, playerOnBasePosition, playerSpeedController * 2f * Time.deltaTime);
        transform.rotation = Quaternion.LerpUnclamped(transform.rotation,
            Quaternion.Euler(0f, 90f, 0f), (playerSpeedController + 2f) / playerSpeedController * Time.deltaTime);

        if (transform.position.y <= playerOnBasePosition.y)
            FinishLandsOnTheBase();
    }
    void FinishLandsOnTheBase()
    {
        isPlayerOnTheBase = true;
        isLandsOnTheBase = false;  
    }

    void UnloadResources()
    {
        if (Input.GetKeyDown(keyToUnloadResources))
            timer.Run(3f);
        else if (Input.GetKey(keyToUnloadResources))
        {
            timer.Update();
            if (timer.Finished)
            {
                Debug.Log("isUnloaded");
                areResourcesBeingUnloaded = true;
                timer.Reset();
            }
        }
        else if (Input.GetKeyUp(keyToUnloadResources))
            timer.Reset();
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
            FinishTakeOffFromBase();
    }
    void FinishTakeOffFromBase()
    {
        isPlayerOnTheBase = false;
        isTakeOffFromBase = false;
        playerScript.IsActive = true;
    }

    bool IsPlayerOnTheBasePosition() => Mathf.Abs(transform.position.x) < basePositionRadius && Mathf.Abs(transform.position.z) < basePositionRadius;
    #endregion
}