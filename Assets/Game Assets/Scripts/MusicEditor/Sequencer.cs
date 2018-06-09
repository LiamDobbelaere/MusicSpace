using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour {
    private GameObject[] pedestalCollections;
    private float timer = 0f;
    private float timerInterval = 0.4f;
    private int currentStep = 0;
    private int maxStep = 3;

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
                Debug.Log(pedestalCollection);
                Debug.Log(pedestalCollection.transform.GetChild(currentStep));
                Debug.Log(pedestalCollection.transform.GetChild(currentStep).GetComponent<Pedestal>());
                pedestalCollection.transform.GetChild(currentStep).GetComponent<Pedestal>().PlayLinked();
            }

            if (++currentStep > maxStep) currentStep = 0;

            timer = 0f;
        }
	}
}
