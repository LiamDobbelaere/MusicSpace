using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCube : MonoBehaviour {
    private Global global;
    private AudioPool audioPool;

    private Material noteMaterial;
    private Material baseMaterial;

    public int instrument = 0;
    public int note = -12;
    public bool linked = false;

    [Tooltip("Permanently locks the cube when it connects to a pedestal.")]
    public bool permaLock = false;

    // Use this for initialization
    void Start () {
        var globalObject = GameObject.Find("Global");
        global = globalObject.GetComponent<Global>();
        audioPool = globalObject.GetComponent<AudioPool>();


        baseMaterial = GetComponent<Renderer>().materials[0];
        noteMaterial = GetComponent<Renderer>().materials[1];

        UpdateNoteDisplay();
	}

    public int GetCurrentNote()
    {
        return Mathx.Mod(note, 12);
    }

    public int GetCurrentOctave()
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

        baseMaterial.SetColor("_Color", global.instrumentColors[instrument]);
        noteMaterial.SetTexture("_MainTex", global.noteTextures[GetCurrentNote()]);
        noteMaterial.SetTexture("_DetailAlbedoMap", global.octaveTextures[(GetCurrentOctave() * 2) - sub]);
        noteMaterial.SetTexture("_DetailMask", global.octaveTextures[(GetCurrentOctave() * 2) - sub]);
    }

    // Update is called once per frame
    void Update () {
        //audioPool.Play(0, pitch);

    }

    public void NextNote()
    {
        if (++note > 23) note = -12;
        UpdateNoteDisplay();
        PlayCube();
    }

    public void PreviousNote()
    {
        if (--note < -12) note = 23;
        UpdateNoteDisplay();
        PlayCube();
    }

    public void PlayCube()
    {
        int realOctave = (int)(GetCurrentOctave() * Mathf.Sign(note));
        audioPool.PlayNote(instrument, realOctave, note);
    }
}
