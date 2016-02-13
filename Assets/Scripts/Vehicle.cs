using UnityEngine;
using System.Collections;

public class Vehicle : BaseTargetBehaviour
{

    public float speed = 1.0f;
    public Vector3 direction = Vector3.right;

	void Start () {
	
	}
	
	void Update ()
	{

	    transform.position += direction*speed*Time.deltaTime;

	}

    public override void ReceiveHit(RaycastHit hitinfo)
    {
        var obj = ObjectPool.Get("SteelHitSystem");
        obj.transform.parent = transform;
        obj.transform.position = hitinfo.point;
        obj.transform.rotation = Quaternion.LookRotation(hitinfo.normal);
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
