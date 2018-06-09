using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    private CharacterController controller;

    private float speed = 5.0f;
    private float sprintSpeed = 7.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;

    private float normalHeight;
    private float crouchHeight;
    private float currentSpeed;
    
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        normalHeight = controller.height;
        crouchHeight = normalHeight / 2f;
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update () {
        currentSpeed = Input.GetButton("Sprint") ? sprintSpeed : speed;

        float mouseInput = Input.GetAxisRaw("Mouse X");
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            controller.height = crouchHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y - (normalHeight - crouchHeight) / 2f, transform.position.z);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            controller.height = normalHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y + (normalHeight - crouchHeight) / 2f, transform.position.z);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * currentSpeed;
    }
}
