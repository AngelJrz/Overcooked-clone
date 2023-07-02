using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 9f;
    [SerializeField] private Animator animator;
    private bool isWalking = false;

    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayerAcction();
        ManageAnimation();
    }

    private void PlayerAcction()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        // Se usa para no aumentar la velocidad exponencialmente en caso de que se pulsen dos teclas al mismo tiempo
        inputVector = inputVector.normalized;

        // No tiene caso mantener un vector de 3 axis ya que el input es de 2 dimensiones, lo que conviene es traducir los valores 
        // y mantener los axis separados
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float rotateSpeed = Time.deltaTime * 7f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed);
    }

    private void ManageAnimation()
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D)
            )
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        animator.SetBool("IsWalking", isWalking);

    }
}
