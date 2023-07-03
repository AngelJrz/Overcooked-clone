using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 9f;
    [SerializeField] private GameInput gameInput;
    private Animator animator;

    void Start() {
        this.animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //**********************************
        // Player movement
        //**********************************
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        float rotateSpeed = Time.deltaTime * 7f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed);

        //**********************************
        // Set animation
        //**********************************
        bool isWalking = moveDir != Vector3.zero;
        animator.SetBool("IsWalking", isWalking);
    }
}
