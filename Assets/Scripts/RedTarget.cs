using UnityEngine;
using System.Collections;

public class RedTarget : BaseTargetBehaviour
{
    [SerializeField]
    private ParticleSystem system;

    [SerializeField]
    private GameObject targetObject;

    public override void ReceiveHit(RaycastHit hit)
    {
        //transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        targetObject.SetActive(false);
        system.Play();
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
