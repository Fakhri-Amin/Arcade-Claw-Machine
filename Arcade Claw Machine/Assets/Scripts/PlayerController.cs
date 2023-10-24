using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Open,
        Close,
        Rising
    }

    [SerializeField] private Rigidbody clawMachineRb;
    [SerializeField] private Rigidbody verticalBarRb;
    [SerializeField] private Rigidbody horizontalBarRb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private const string CLAWANIMATIONNAME = "IsGrab";

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.OnGrabPressed += GameInput_OnGrabPressed;

        state = State.Open;
    }

    private void GameInput_OnGrabPressed()
    {
        animator.SetBool(CLAWANIMATIONNAME, true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Open:
                HandleMovement();
                break;
            case State.Close:
                break;
            default:
                break;
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection.x != 0)
        {
            verticalBarRb.AddForce(moveDirection * moveSpeed);
        }

        if (moveDirection.z != 0)
        {
            horizontalBarRb.AddForce(moveDirection * moveSpeed);
        }

        clawMachineRb.AddForce(moveDirection * moveSpeed);
    }

    // private IEnumerator RotateTheArmTo(float targetAngle)
    // {
    //     float elapsedTime = 0f;
    //     float currentXRotation = transform.rotation.x;
    //     Vector3 targetRotation = new Vector3(targetAngle, )

    //     while (elapsedTime < rotateSpeed)
    //     {
    //         transform.rotation = Vector3.Lerp(transform.rotation, TargetPosition, elapsedTime / rotateSpeed);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     transform.position = TargetPosition;
    // }
}
