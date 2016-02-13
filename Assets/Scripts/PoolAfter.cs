using UnityEngine;
using System.Collections;

public class PoolAfter : MonoBehaviour
{

    public float seconds = 1f;
    public bool removeSelf = true;

    private float endTime;
    
	void OnEnable()
	{
	    endTime = Time.time + seconds;
	}

	void Update ()
    {
	    if (endTime < Time.time)
	    {
	        ObjectPool.Add(gameObject);

            if (removeSelf)
                Destroy(this); // remove this component
	    }
	}
}
