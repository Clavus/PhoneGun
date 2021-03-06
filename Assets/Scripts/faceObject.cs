﻿using UnityEngine;
using System.Collections;

///public class objectFaceCam : MonoBehaviour
public class faceObject : MonoBehaviour
{
    //public Vector3 lookAtMe;
    public Transform lookAtMe;
    public Vector3 lookOffset;
    private Vector3 lookAtMyVector;
    public float lookDistance = 6f;

    [SerializeField]
    private float distance;

    void Start()
    {
        //if (lookAtMyVector)
        //{
        //    lookOffset = new Vector3(0, 0, 0);
        //}

        if (lookAtMe == null)
        {
            lookAtMe = Camera.main.transform;
            lookAtMyVector = lookAtMe.position;
        }
    }



    // Update is called once per frame
    void Update ()
    {
        LookAtObject();
    }


    void LookAtObject()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (distance <= lookDistance)
        {
            this.transform.LookAt(lookAtMe.position);
            this.transform.Rotate(lookOffset);
        }
    }
}
