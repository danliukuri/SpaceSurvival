using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Transform player;
    PlayerController playerController;
    Animator animator;
    bool isTakingOffFromTheBase;
    bool isLandsOnTheBase;
    bool isPlayerOnTheBase;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        TakeOffFromBaseAnimationStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerOnTheBase && Input.GetKeyDown(KeyCode.Space))
        {
            //playerController.MoveTo(new Vector3());
            LandsOnTheBaseAnimationStart();
        }
        else if (isPlayerOnTheBase && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isLandsOnTheBase", false);
            TakeOffFromBaseAnimationStart();
        }
    }
    void TakeOffFromBaseAnimationStart()
    {
        animator.enabled = isTakingOffFromTheBase = true;
        playerController.IsActive = false;
        isPlayerOnTheBase = false;

        animator.SetBool("isTakingOffFromTheBase", true);
    }
    void TakeOffFromBaseAnimationExit()
    {
        animator.enabled = isTakingOffFromTheBase = false;
        playerController.IsActive = true;

        animator.SetBool("isTakingOffFromTheBase", false);
    }

    void LandsOnTheBaseAnimationStart()
    {
        animator.enabled = isLandsOnTheBase = true;
        playerController.IsActive = false;

        animator.SetBool("isLandsOnTheBase", true);
    }
    void LandsOnTheBaseAnimationExit()
    {
        isLandsOnTheBase = false;
        isPlayerOnTheBase = true;
    }
}
