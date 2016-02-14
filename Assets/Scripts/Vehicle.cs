using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{

    public float speed = 2.0f;
    public float accelerate = 0.1555f;
    public float maxSpeed = 30f;
    public Vector3 direction = Vector3.right;

	void Start () {
	
	}
	
	void Update ()
	{

	    transform.position += direction*speed*Time.deltaTime;

	    if (speed > 0)
	        speed = Mathf.Min(maxSpeed, speed + accelerate*Time.deltaTime);

	}

}
