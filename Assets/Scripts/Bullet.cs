using UnityEngine;
using System.Collections;

public class Bullet : BaseTargetBehaviour
{
    public override void ReceiveHit()
    {
        //transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        GetComponent<Weapon_Health>().setDamage(10f);
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
