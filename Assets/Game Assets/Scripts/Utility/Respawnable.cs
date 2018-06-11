using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour {
    private Vector3 startPosition;
    
	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnTrigger"))
            transform.position = startPosition;
    }
}
