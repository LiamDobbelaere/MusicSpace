using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCube : MonoBehaviour {
    private Global global;
    private AudioPool audioPool;

    private int note = -12;
    private Material noteMaterial;

	// Use this for initialization
	void Start () {
        var globalObject = GameObject.Find("Global");
        global = globalObject.GetComponent<Global>();
        audioPool = globalObject.GetComponent<AudioPool>();

        noteMaterial = GetComponent<Renderer>().materials[1];

        UpdateNoteDisplay();
	}

    int GetCurrentNote()
    {
        return Mathx.Mod(note, 12);
    }

    int GetCurrentOctave()
    {
        if (note < 0)
        {
            return (int)Mathf.Ceil(Mathf.Abs(note) / 12f);
        }
        else
        {
            return (int)Mathf.Floor(Mathf.Abs(note) / 12f);
        }
    }

    void UpdateNoteDisplay()
    {
        int sub = 0;
        if (note < 0) sub = 1;

        noteMaterial.SetTexture("_MainTex", global.noteTextures[GetCurrentNote()]);
        noteMaterial.SetTexture("_DetailAlbedoMap", global.octaveTextures[(GetCurrentOctave() * 2) - sub]);
    }

    // Update is called once per frame
    void Update () {
        //audioPool.Play(0, pitch);

	}

    private void OnMouseDown()
    {
        if (++note > 23) note = -12;
        UpdateNoteDisplay();
    }

    public void PlayCube()
    {
        int realOctave = (int)(GetCurrentOctave() * Mathf.Sign(note));
        audioPool.PlayNote(0, realOctave, note);
    }
}
