using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPedestals : MonoBehaviour {
    public PedestalCollection[] pedestals;
    private Sequencer sequencer;

	// Use this for initialization
	void Start () {
        sequencer = GameObject.Find("Global").GetComponent<Sequencer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (PedestalCollection pedestal in pedestals)
            {
                pedestal.isActive = true;
                sequencer.ResetSequencer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (PedestalCollection pedestal in pedestals)
            {
                pedestal.isActive = false;
            }
        }
    }
}
