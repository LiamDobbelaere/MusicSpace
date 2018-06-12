using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour {
    public Vector3 startPosition;
    private Quaternion startRotation;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        startRotation = transform.rotation;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnTrigger"))
        {
            transform.position = startPosition;
            transform.rotation = startRotation;

            var r = GetComponent<Rigidbody>();

            if (r != null)
            {
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
            }
        }
    }
}
