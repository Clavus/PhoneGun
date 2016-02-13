using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class NoVRMouseLook : MonoBehaviour
{

    public float sensitivity = 1.0f;

    void Start()
    {
        if (!VRSettings.enabled)
            Cursor.lockState = CursorLockMode.Locked;
    }

	void Update ()
	{
        // cancel in case VR is active
	    if (VRSettings.enabled)
	        return;

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

        if (Cursor.lockState == CursorLockMode.Locked)
	    {
            transform.Rotate(Vector3.right, sensitivity * Input.GetAxis("Mouse Y") * -1);
            transform.Rotate(transform.InverseTransformVector(Vector3.up), sensitivity * Input.GetAxis("Mouse X"));
        }
    }
}
