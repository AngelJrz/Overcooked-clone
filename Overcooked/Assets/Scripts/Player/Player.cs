using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 9f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    private Animator animator;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;


    private void Awake() {
        if (Instance != null) {
            Debug.Log("There is more than one player");
        }
        Instance = this;
    }

    void Start() {
        this.animator = GetComponentInChildren<Animator>();
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HandleInteractions();
        HandleMovement();
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 1f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, layerMask)) {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {

                //if (clearCounter != selectedCounter) {
                //    selectedCounter = clearCounter;
                //    SetSelectedCounter(selectedCounter);
                //}

                // If the player returns to the same counter shows the selected prefab
                selectedCounter = clearCounter;
                SetSelectedCounter(clearCounter);
            } else {
                SetSelectedCounter(null);
            }
        }else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = .6f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerRadius);
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //**********************************
        // Player movement
        //**********************************
        if (!canMove) {
            // cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            } else {
                //Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                } else {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        float rotateSpeed = Time.deltaTime * 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed);

        //**********************************
        // Set animation
        //**********************************
        bool isWalking = moveDir != Vector3.zero;
        animator.SetBool("IsWalking", isWalking);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }
}
