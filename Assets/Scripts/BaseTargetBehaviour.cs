using UnityEngine;
using System.Collections;

public abstract class BaseTargetBehaviour : MonoBehaviour
{

    public abstract void ReceiveHit(RaycastHit hitinfo);
    public abstract TargetType GetTargetType();

}
