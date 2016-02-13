using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{

    public float speed = 1.0f;
    public Vector3 direction = Vector3.right;

	void Start () {
	
	}
	
	void Update ()
	{

	    transform.position += direction*speed*Time.deltaTime;

	}
}
