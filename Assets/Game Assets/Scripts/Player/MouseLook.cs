using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    private float yRotation;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        yRotation += Input.GetAxisRaw("Mouse Y");
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localEulerAngles = new Vector3(-yRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
