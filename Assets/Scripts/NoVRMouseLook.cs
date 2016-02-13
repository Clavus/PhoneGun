using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class NoVRMouseLook : MonoBehaviour
{

    public float sensitivity = 1.0f;

	void Update ()
	{
        // cancel in case VR is active
	    if (VRSettings.enabled)
	        return;

        if (Cursor.lockState == CursorLockMode.Locked)
	    {
            transform.Rotate(Vector3.right, sensitivity * Input.GetAxis("Mouse Y") * -1);
            transform.Rotate(transform.InverseTransformVector(Vector3.up), sensitivity * Input.GetAxis("Mouse X"));
        }
    }
}
