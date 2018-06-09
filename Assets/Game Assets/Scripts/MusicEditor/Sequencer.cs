using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour {
    private GameObject[] pedestalCollections;
    private float timer = 0f;
    private float timerInterval = 0.4f;
    private int currentStep = 0;
    private int maxStep = 31;

	// Use this for initialization
	void Start () {
        pedestalCollections = GameObject.FindGameObjectsWithTag("PedestalCollection");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > timerInterval)
        {
            foreach (GameObject pedestalCollection in pedestalCollections)
            {
                pedestalCollection.transform.GetChild(Mathx.Mod(currentStep, pedestalCollection.transform.childCount))
                    .GetComponent<Pedestal>().PlayLinked();
            }

            if (++currentStep > maxStep) currentStep = 0;

            timer = 0f;
        }
	}
}
