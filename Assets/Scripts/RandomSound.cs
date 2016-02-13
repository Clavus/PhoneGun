using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{

    public AudioClip[] selection;
    public float pitchVariation;

	// Use this for initialization
	void Start ()
	{
	    var source = GetComponent<AudioSource>();

	    source.pitch = source.pitch - pitchVariation + pitchVariation*2*Random.value;

        if (selection.Length > 0)
	        source.clip = selection[Random.Range(0, selection.Length)];
	}
	
}
