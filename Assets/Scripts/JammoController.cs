using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class JammoController : MonoBehaviour
{



    public AudioSource footstep1;
    public AudioSource footstep2;

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRun;
    bool isMovementPressed;
    bool isRunPressed;
    public float rotationFactorPerFrame = 0.1f;
    public float runSpeedFactor = 2.5f;


    int isWalkingHash;
    int isRunningHash;


    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRun.x = currentMovementInput.x * runSpeedFactor;
        currentRun.z = currentMovementInput.y * runSpeedFactor;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }


    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundGravity = -0.1f;
            currentMovement.y = groundGravity;
            currentRun.y = groundGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRun.y += gravity;
        }
    }
    void HandleRotation()
    {
        Vector3 lookAt;
        lookAt.x = currentMovement.x;
        lookAt.y = 0.0f;
        lookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }
    void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }



    }

    void Update()
    {
        HandleRotation();
        HandleAnimation();
        if (isRunPressed)
        {
            characterController.Move(currentRun * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    public void JammoStepSoundWood_1()
    {
        footstep1.Play();
    }
    public void JammoStepSoundWood_2()
    {
        footstep1.Play();
    }
}
