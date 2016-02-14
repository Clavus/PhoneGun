using UnityEngine;
using System.Collections;

public class MoveInDirection : MonoBehaviour
{

    public Vector3 direction = Vector3.left;
    public float speed = 1;
    
	void Update ()
	{

	    transform.position += direction*speed*Time.deltaTime;

	}
}
