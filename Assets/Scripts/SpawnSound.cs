using UnityEngine;
using System.Collections;

public class SpawnSound : MonoBehaviour
{

    public AudioSource sound;
    
	void OnEnable()
	{

	    if (sound != null)
	    {
            GameObject obj = (GameObject)Instantiate(sound.gameObject, transform.position, Quaternion.identity);
	        obj.transform.parent = transform;
	    }     

	}
	
}
