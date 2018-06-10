using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableHint : MonoBehaviour {
    PlayerController controller;
    Text text;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = controller.interactMessage.Length > 0 ? "[Left mouse button] " + controller.interactMessage : "";
	}
}
