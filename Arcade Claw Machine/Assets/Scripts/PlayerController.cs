using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public event Action OnGrabbedObjectReleased;


    private enum State
    {
        Open,
        Close,
        Rising
    }

    [SerializeField] private Rigidbody clawMachineRb;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private const string CLAWGRABANIMATION = "IsGrab";
    [SerializeField] private float targetClawMachineYPosition = 2.25f;
    [SerializeField] private float minZPosition;
    [SerializeField] private float maxZPosition;
    [SerializeField] private float minXPosition;
    [SerializeField] private float maxXPosition;


    private State state;
    private float startingClawMachineYPosition;
    private bool isGrabbed;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.OnGrabPressed += GameInput_OnGrabPressed;
        state = State.Open;
        startingClawMachineYPosition = clawMachineRb.transform.position.y;
    }

    private void GameInput_OnGrabPressed()
    {
        isGrabbed = !isGrabbed;

        if (isGrabbed && state == State.Open)
        {
            StartCoroutine(MoveClawMachine(targetClawMachineYPosition, () => { StartCoroutine(SetStateTo(State.Close, 0)); }));
        }
        else
        {
            OnGrabbedObjectReleased();
            animator.SetBool(CLAWGRABANIMATION, false);
        }
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
                animator.SetBool(CLAWGRABANIMATION, true);
                StartCoroutine(SetStateTo(State.Rising, 1f));
                break;
            case State.Rising:
                StartCoroutine(MoveClawMachine(startingClawMachineYPosition, () =>
                {
                    StartCoroutine(SetStateTo(State.Open, 0));
                }));
                break;
            default:
                break;
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        moveDirection.x = Mathf.Clamp(moveDirection.x, minXPosition, maxXPosition);
        moveDirection.z = Mathf.Clamp(moveDirection.z, minZPosition, maxZPosition);

        clawMachineRb.AddForce(moveDirection * moveSpeed);
    }

    private IEnumerator MoveClawMachine(float targetYPosition, Action onActionComplete)
    {
        float elapsedTime = 0f;
        float verticalMoveSpeed = 0.6f;
        Vector3 currentPosition = clawMachineRb.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, targetYPosition, currentPosition.z);

        while (elapsedTime < verticalMoveSpeed)
        {
            clawMachineRb.transform.position = Vector3.Lerp(currentPosition, targetPosition, elapsedTime / verticalMoveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        clawMachineRb.transform.position = targetPosition;
        onActionComplete();
    }

    public bool IsCloseState()
    {
        return state == State.Close;
    }

    public bool IsRisingState()
    {
        return state == State.Rising;
    }

    private IEnumerator SetStateTo(State state, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.state = state;
    }
}
