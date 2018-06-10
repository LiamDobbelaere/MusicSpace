using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {
    MusicCube linkedCube;
    Material baseMaterial;
    Color startColor;

    float changeTime = 0f;

	// Use this for initialization
	void Start () {
        baseMaterial = GetComponent<Renderer>().materials[0];
        startColor = baseMaterial.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
        if (changeTime > 0f)
        {
            changeTime -= Time.deltaTime;
        }
        else
        {
            baseMaterial.SetColor("_Color", Color.Lerp(baseMaterial.GetColor("_Color"), startColor, 10f * Time.deltaTime));
        }
    }

    public void PlayLinked()
    {
        changeTime = 0.2f;
        baseMaterial.SetColor("_Color", new Color(1f - startColor.r, 1f - startColor.g, 1f - startColor.b));

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
