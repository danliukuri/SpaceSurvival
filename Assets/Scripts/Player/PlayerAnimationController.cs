using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    bool isTakingOffFromTheBase;
    Transform player;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.Play("PlayerCreation");
        isTakingOffFromTheBase = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTakingOffFromTheBase && player.position.y >= -0.257f)
        {
            animator.enabled = isTakingOffFromTheBase = false;
            playerController.IsActive = true;
        }
    }
}
