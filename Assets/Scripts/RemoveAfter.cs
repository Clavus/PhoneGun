using UnityEngine;
using System.Collections;

public class RemoveAfter : MonoBehaviour
{

    public float duration = 1.0f;

    private float endTime;

	// Use this for initialization
	void Start ()
	{

	    endTime = Time.time + duration;

	}
	
	// Update is called once per frame
	void Update () {
	
        if (endTime < Time.time)
            Destroy(gameObject);

	}
}
