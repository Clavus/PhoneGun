﻿using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class Player : MonoBehaviour
{

    [SerializeField]
    private BulletBelt bulletBelt;

    [SerializeField]
    private AudioSource shootAudio;

    [SerializeField]
    private AudioSource dryAudio;

    private int targetLayers;

	// Use this for initialization
	void Start ()
	{
        Cursor.lockState = CursorLockMode.Locked;
	    targetLayers = LayerMask.GetMask("Entity", "Geometry");

	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButtonDown("Fire1") && !GameManager.IsGameOver())
	    {
            //Debug.Log("Firing");
            
            Ray r = new Ray(transform.position, transform.forward);
	        RaycastHit hitinfo;
            Debug.DrawRay(r.origin, r.direction * 10f, Color.red, 2f);

	        bool hitTrigger = false;
	        BaseTargetBehaviour target;

	        if (Physics.Raycast(r, out hitinfo, 1000f, targetLayers))
	        {
                //Debug.Log("Hit");
                // get base game object
                GameObject obj = hitinfo.collider.gameObject;
	            if (hitinfo.collider.attachedRigidbody != null)
	                obj = hitinfo.collider.attachedRigidbody.gameObject;

                // find target behaviour
	            target = obj.GetComponent<BaseTargetBehaviour>();
	            if (target != null)
	            {
	                if (target.GetTargetType() == TargetType.Enemy && bulletBelt.GetBulletsLeft() > 0)
	                    target.ReceiveHit(hitinfo); // trigger hit
	                else if (target.GetTargetType() == TargetType.Trigger)
	                {
	                    hitTrigger = true;
	                    target.ReceiveHit(hitinfo);
	                }

	            }
	            else if (bulletBelt.GetBulletsLeft() > 0)
	            {
	                BaseHitEffect(hitinfo);
	            }
                    
            }

	        if (!hitTrigger)
	        {
	            if (bulletBelt.GetBulletsLeft() > 0)
	            {
	                bulletBelt.UseBullet();

	                shootAudio.pitch = 0.9f + 0.2f*Random.value;
	                shootAudio.Play();
	            }
	            else
	            {
	                dryAudio.Play();
	            }
            }

        }

        // lock mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // unlock mouse
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

	    if (Input.GetKeyDown(KeyCode.F2) && VRSettings.enabled)
	    {
	        InputTracking.Recenter();
	    }

    }

    void BaseHitEffect(RaycastHit hitinfo)
    {
        var obj = ObjectPool.Get("SteelHitSystem");
        obj.transform.parent = hitinfo.collider.transform;
        obj.transform.position = hitinfo.point;
        obj.transform.rotation = Quaternion.LookRotation(hitinfo.normal);
    }
}
