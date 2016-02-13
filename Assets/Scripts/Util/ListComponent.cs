using System.Collections.Generic;
using UnityEngine;

public abstract class ListComponent<T> : MonoBehaviour where T : MonoBehaviour
{
    public static List<T> instanceList = new List<T>();

    protected virtual void OnEnable()
    {
        instanceList.Add(this as T);
    }

    protected virtual void OnDisable()
    {
        instanceList.Remove(this as T);
    }
}