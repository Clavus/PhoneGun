﻿using UnityEngine;
using System.Collections;

public abstract class BaseTargetBehaviour : MonoBehaviour
{

    public abstract void ReceiveHit();
    public abstract TargetType GetTargetType();

}
