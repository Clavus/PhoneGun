﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Drone : BaseTargetBehaviour
{
    public override void ReceiveHit(RaycastHit hit)
    {
        //transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        GetComponent<Weapon_Health>().setDamage(10f);
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
