using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {
    MusicCube linkedCube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PlayLinked()
    {
        if (linkedCube != null)
        {
            linkedCube.PlayCube();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        MusicCube musicCube = collision.gameObject.GetComponent<MusicCube>();

        if (musicCube != null)
        {
            linkedCube = musicCube;
        }

        Debug.Log("Linked!");
    }

    private void OnCollisionStay(Collision collision)
    {
        MusicCube musicCube = collision.gameObject.GetComponent<MusicCube>();

        if (linkedCube == musicCube)
        {
            collision.gameObject.transform.position = Vector3.Lerp(collision.gameObject.transform.position, 
                new Vector3(
                    gameObject.transform.position.x, 
                    collision.gameObject.transform.position.y, 
                    gameObject.transform.position.z), 
                0.1f);
            /*collision.gameObject.transform.localEulerAngles = Vector3.Lerp(
                collision.gameObject.transform.localEulerAngles, 
                Vector3.zero, 0.1f);*/

            collision.gameObject.transform.eulerAngles = Vector3.left * 90f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        MusicCube musicCube = collision.gameObject.GetComponent<MusicCube>();

        if (linkedCube == musicCube)
        {
            linkedCube = null;
        }

        Debug.Log("Unlinked!");
    }
}
