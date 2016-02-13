using UnityEngine;

public abstract class SingletonComponent<T> : SingletonComponent where T : MonoBehaviour
{
    public static T instance { get; private set; }
    public virtual bool OverrideInstanceOnNewObject { get { return true; } }

    public override void Setup()
    {
        if (instance == null || (OverrideInstanceOnNewObject && instance != this))
            instance = this as T;

        //Debug.Log("Set " + instance.GetType().FullName + " " + instance.GetInstanceID() + " as instance!");
    }

    public override void Clear()
    {
        if (instance == this) instance = null;
    }
}

public abstract class SingletonComponent : MonoBehaviour
{
    public abstract void Setup();
    public abstract void Clear();

    protected virtual void Awake()
    {
        Setup();
    }

    protected virtual void OnDestroy()
    {
        Clear();
    }
}