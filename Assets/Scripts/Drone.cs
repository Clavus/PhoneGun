using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Drone : BaseTargetBehaviour
{
    public override void ReceiveHit()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        GetComponent<HealthManager>().setDamage(10f);
    }
}
