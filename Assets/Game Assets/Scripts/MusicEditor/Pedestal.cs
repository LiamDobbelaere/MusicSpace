using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {
    MusicCube linkedCube;
    Material baseMaterial;
    Color startColor;

	// Use this for initialization
	void Start () {
        baseMaterial = GetComponent<Renderer>().materials[0];
        startColor = baseMaterial.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
        baseMaterial.SetColor("_Color", Color.Lerp(baseMaterial.GetColor("_Color"), startColor, 0.05f));
	}

    public void PlayLinked()
    {
        baseMaterial.SetColor("_Color", new Color(0.1f, 0.1f, 0.1f));

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
    }
}
