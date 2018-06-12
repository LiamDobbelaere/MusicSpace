using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {
    public MusicCube linkedCube;
    private Material baseMaterial;
    private Color startColor;
    private float changeTime = 0f;
    
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
        baseMaterial.SetColor("_Color", Color.white);

        if (linkedCube != null)
        {
            linkedCube.PlayCube();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        MusicCube musicCube = collision.gameObject.GetComponent<MusicCube>();

        if (musicCube != null && linkedCube == null && !musicCube.linked)
        {
            linkedCube = musicCube;
            linkedCube.linked = true;
            linkedCube.gameObject.layer = LayerMask.NameToLayer("MusicCubeLinked");
            /*else
            {
                Debug.Log("Force application");
                musicCube.gameObject.GetComponent<Rigidbody>().AddForce(
                    new Vector3(Random.Range(-500f, 500f), Random.Range(300f, 500f), Random.Range(-500f, 500f))
                    );
            }*/
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
            linkedCube.linked = false;
            linkedCube.gameObject.layer = LayerMask.NameToLayer("MusicCube");
            linkedCube = null;
        }
    }
}
