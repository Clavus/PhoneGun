using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TargetExample : BaseTargetBehaviour {

    public override void ReceiveHit()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 2f);
       
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
