using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Drone : BaseTargetBehaviour
{
    public float setDamage = 34f;
    public override void ReceiveHit(RaycastHit hit)
    {
        //transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        GetComponent<HealthManager>().setDamage(setDamage);
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
