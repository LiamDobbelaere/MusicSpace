using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBox : MonoBehaviour {
    public Color color;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 0.6f);
        Gizmos.DrawWireCube(transform.position, transform.GetComponent<BoxCollider>().bounds.size);
        Gizmos.color = new Color(color.r, color.g, color.b, 0.2f);
        Gizmos.DrawCube(transform.position, transform.GetComponent<BoxCollider>().bounds.size);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
