using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour {
    [System.Serializable]
    public class Instrument
    {
        public AudioClip[] notes;
    }

    public Instrument[] instruments;
    
    private AudioSource[] audioSources;
    private int currentSource = 0;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 64; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }

        audioSources = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayNote(int instrument, int octave, int note)
    {
        AudioSource source = audioSources[currentSource];
        
        //source.pitch = (1f + octave) + note * (1f / 12f);
        source.PlayOneShot(instruments[instrument].notes[note + 12]);
        
        if (++currentSource >= audioSources.Length) currentSource = 0;
    }
}
