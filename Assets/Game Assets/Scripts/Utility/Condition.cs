using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour {
    [System.Serializable]
    public class PedestalCondition
    {
        public PedestalCollection pedestalCollection;
        public int[] noteValues;
    }

    public AutoBridge bridge;
    public PedestalCondition[] pedestals;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool conditionMet = true;

		foreach (PedestalCondition pc in pedestals)
        {
            for (int i = 0; i < pc.noteValues.Length; i++)
            {
                var linkedCube = pc.pedestalCollection.transform.GetChild(i).GetComponent<Pedestal>().linkedCube;

                if (linkedCube != null)
                {
                    if (pc.noteValues[i] != linkedCube.note)
                        conditionMet = false;
                }
                else
                {
                    if (pc.noteValues[i] != -255)
                        conditionMet = false;
                }
            }
        }

        if (conditionMet)
        {
            bridge.Unroll();
            Destroy(gameObject);
        }
	}
}
