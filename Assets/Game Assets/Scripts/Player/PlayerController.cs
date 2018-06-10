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

    public string interactMessage;

    private Transform pickupPoint;
    private Rigidbody pickedupObject;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        pickupPoint = Camera.main.transform.Find("PickupPoint");
        normalHeight = controller.height;
        crouchHeight = normalHeight / 2f;
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update () {
        UpdateMovement();
        UpdateInteraction();
    }

    void UpdateMovement()
    {
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

    void UpdateInteraction()
    {
        interactMessage = "";

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            Transform objectHit = hit.transform;

            if (hit.transform.GetComponent<MusicCube>() != null)
            {
                if (pickedupObject == null)
                {
                    interactMessage = "Pick up";

                    if (Input.GetMouseButtonDown(0))
                    {
                        pickedupObject = hit.transform.GetComponent<Rigidbody>();
                        pickedupObject.isKinematic = true;
                        //pickedupObject.GetComponent<BoxCollider>().enabled = false;
                    }
                }

                var mc = hit.transform.GetComponent<MusicCube>();
                if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                {
                    mc.NextNote();
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                {
                    mc.PreviousNote();
                }
            }
        }

        if (pickedupObject != null)
        {
            pickedupObject.MovePosition(pickupPoint.position);

            if (Input.GetMouseButtonUp(0))
            {
                pickedupObject.isKinematic = false;
                //pickedupObject.GetComponent<BoxCollider>().enabled = true;
                pickedupObject = null;
            }
        }
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
