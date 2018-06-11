using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {
    private Material material;
    public Vector2 offsetStep;
    public int materialId;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().materials[materialId];
    }
	
	// Update is called once per frame
	void Update () {
        material.mainTextureOffset += offsetStep * Time.deltaTime;
    }
}
