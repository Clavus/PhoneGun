using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private int targetLayers;

	// Use this for initialization
	void Start ()
	{

	    targetLayers = LayerMask.GetMask("Entity");

	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButtonDown("Fire1"))
	    {
            Debug.Log("Firing");
            Ray r = new Ray(transform.position, transform.forward * 100f);
	        RaycastHit hitinfo;
            Debug.DrawRay(r.origin, r.direction * 10f, Color.red, 2f);
	        if (Physics.Raycast(r, out hitinfo, 1000f, targetLayers))
	        {
                Debug.Log("Hit");
                // get base game object
                GameObject obj = hitinfo.collider.gameObject;
	            if (hitinfo.collider.attachedRigidbody != null)
	                obj = hitinfo.collider.attachedRigidbody.gameObject;

                // find target behaviour
	            var target = obj.GetComponent<BaseTargetBehaviour>();
                if (target != null)
                    target.ReceiveHit(); // trigger hit
	        }
	    }


	}
}
