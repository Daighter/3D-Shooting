using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f; 

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDir;
    private float moveSpeed;
    private float ySpeed = 0f;
    private bool isGrounded = true;
    private bool isWalking = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void Move()
    {
        // 월드기준 움직임
        // controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // 로컬기준 움직임
        if (moveDir.magnitude <= 0)         // 안움직임
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f);
        else if (!isWalking)
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
        else
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed,  0.5f);


        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        animator.SetFloat("xSpeed", moveDir.x, 0.1f, Time.deltaTime);
        animator.SetFloat("zSpeed", moveDir.z, 0.1f, Time.deltaTime);
        animator.SetFloat("speed", moveSpeed);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }

    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (isGrounded && ySpeed < 0)
            ySpeed = 0f;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnJump(InputValue value)
    {
        if (isGrounded)
            ySpeed = jumpSpeed;
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.5f))
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void OnWalk(InputValue value)
    {
        isWalking = value.isPressed;
    }
}
