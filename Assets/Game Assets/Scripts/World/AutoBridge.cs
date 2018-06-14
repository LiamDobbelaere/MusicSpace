using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBridge : MonoBehaviour {
    public int count;
    public Vector3 direction;
    public bool autoUnroll = false;

    private bool triggered;


	// Use this for initialization
	void Start () {
        triggered = false;
        if (autoUnroll) Unroll();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Unroll()
    {
        if (triggered) return;

        for (int i = 0; i < count; i++)
        {
            var newInstance = Instantiate(this);
            Destroy(newInstance.GetComponent<AutoBridge>());
            newInstance.gameObject.AddComponent<MoveTarget>().target =
                (newInstance.transform.position + (new Vector3(3f * i * direction.x, 3f * i * direction.y, 3f * i * direction.z)));
        }

        triggered = true;
    }
}
