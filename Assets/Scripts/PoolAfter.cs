using UnityEngine;
using System.Collections;

public class PoolAfter : MonoBehaviour
{

    public float seconds = 1f;

    private float endTime;
    
	void Start ()
	{
	    endTime = Time.time + seconds;
	}

	void Update ()
    {
	    if (endTime < Time.time)
	    {
	        ObjectPool.Add(gameObject);
            Destroy(this); // remove this component
	    }
	}
}
