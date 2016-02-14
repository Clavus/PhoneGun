using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

// Taken and improved from: http://forum.unity3d.com/threads/simple-reusable-object-pool-help-limit-your-instantiations.76851/

/// <summary>
///     Repository of commonly used prefabs.
/// </summary>
[AddComponentMenu("Gameplay/ObjectPool")]
public class ObjectPool : SingletonComponent<ObjectPool>
{
    #region member

    /// <summary>
    ///     Member class for a prefab entered into the object pool
    /// </summary>
    [Serializable]
    public class ObjectPoolEntry
    {
        /// <summary>
        ///     The object to pre-instantiate
        /// </summary>
        [SerializeField]
        public GameObject Prefab;

        /// <summary>
        ///     Amount of object to pre-instantiate at start
        /// </summary>
        [SerializeField]
        public int StartBufferSize;

        /// <summary>
        ///     The object pool
        /// </summary>
        [HideInInspector]
        public Queue<GameObject> Pool;

        /// <summary>
        ///     If the object is a networked object
        /// </summary>
        [HideInInspector]
        public bool IsNetworked;
    }

    #endregion

    /// <summary>
    ///     The object prefabs which the pool can handle.
    /// </summary>
    public ObjectPoolEntry[] pooledObjects;

    // The faster lookup dictionary
    private Dictionary<string, ObjectPoolEntry> entries;

    void Start()
    {
        // Loop through the object prefabs and make a new queue for each one.
        // Store everything in the entries dictionary for faster lookup.
        entries = new Dictionary<string, ObjectPoolEntry>();

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            ObjectPoolEntry pooled = pooledObjects[i];
            if (pooled.Prefab == null)
                continue;

            string pname = pooled.Prefab.name;
            if (entries.ContainsKey(pname))
            {
                Debug.LogError("Object pool contains multiple entries for name '" + pname + "'");
                continue;
            }

            // Create the repository
            pooled.Pool = new Queue<GameObject>();
            pooled.IsNetworked = pooled.Prefab.HasComponent<NetworkIdentity>();
            entries.Add(pname, pooled);

            // Fill the buffer
            for (int n = 0; n < pooled.StartBufferSize; n++)
            {
                pooled.Prefab.SetActive(false); // deactivate before spawn
                GameObject newObj = (GameObject)Instantiate(pooled.Prefab, Vector3.zero, Quaternion.identity);
                newObj.name = pooled.Prefab.name;
                
                newObj.transform.parent = transform;
                newObj.SetActive(false);

                pooled.Pool.Enqueue(newObj);
            }
        }
    }

    /// <summary>
    ///     Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no
    ///     objects of that type in the pool
    ///     then null will be returned.
    /// </summary>
    /// <returns>
    ///     The object for type.
    /// </returns>
    /// <param name='prefabName'>
    ///     Object type.
    /// </param>
    /// <param name='onlyPooled'>
    ///     If true, it will only return an object if there is one currently pooled.
    /// </param>
    /// <param name='position'>
    ///     New position of the object.
    /// </param>
    /// <param name='rotation'>
    ///     New rotation of the object.
    /// </param>
    public static GameObject Get(string prefabName, bool onlyPooled, Vector3 position, Quaternion rotation)
    {
        return instance.GetInternal(prefabName, onlyPooled, position, rotation);
    }

    public static GameObject Get(GameObject obj, bool onlyPooled, Vector3 position, Quaternion rotation)
    {
        return instance.GetInternal(obj.name, onlyPooled, position, rotation);
    }

    public static GameObject Get(Component obj, bool onlyPooled, Vector3 position, Quaternion rotation)
    {
        return instance.GetInternal(obj.name, onlyPooled, position, rotation);
    }

    public static GameObject Get(string prefabName, Vector3 position, Quaternion rotation)
    {
        return instance.GetInternal(prefabName, false, position, rotation);
    }

    public static GameObject Get(GameObject obj, Vector3 position, Quaternion rotation)
    {
        return instance.GetInternal(obj.name, false, position, rotation);
    }

    public static GameObject Get(Component obj, Vector3 position, Quaternion rotation)
    {
        return instance.GetInternal(obj.name, false, position, rotation);
    }

    public static GameObject Get(string prefabName)
    {
        return instance.GetInternal(prefabName, false, Vector3.zero, Quaternion.identity);
    }

    public static GameObject Get(GameObject obj)
    {
        return instance.GetInternal(obj.name, false, Vector3.zero, Quaternion.identity);
    }

    public static GameObject Get(Component obj)
    {
        return instance.GetInternal(obj.name, false, Vector3.zero, Quaternion.identity);
    }

    private GameObject GetInternal(string prefabName, bool onlyPooled, Vector3 position, Quaternion rotation)
    {
        ObjectPoolEntry pooled;

        if (!entries.TryGetValue(prefabName, out pooled))
        {
            Debug.LogError("'" + prefabName + "' is not a pooled object!");
            return null;
        }

        GameObject prefab = pooled.Prefab;
        Queue<GameObject> pool = pooled.Pool;

        if (pool.Count > 0)
        {
            GameObject pooledObj = pool.Dequeue();
            //pooledObject.transform.parent = null;
            pooledObj.transform.position = position;
            pooledObj.transform.rotation = rotation;
            pooledObj.SetActive(true);
            
            return pooledObj;
        }

        if (!onlyPooled)
        {
            GameObject newObj = (GameObject)Instantiate(prefab, position, rotation);
            newObj.name = prefab.name;
            newObj.transform.parent = transform;
            newObj.SetActive(true);
            
            return newObj;
        }

        // Only returned if onlyPooled is set to true and we ran out of pooled objects
        return null;
    }

    /// <summary>
    ///     Pools the object specified.
    /// </summary>
    /// <param name='obj'>
    ///     Object to be pooled.
    /// </param>
    public static void Add(GameObject obj)
    {
        instance.AddInternal(obj);
    }

    public static void Add(Component obj)
    {
        instance.AddInternal(obj.gameObject);
    }

    private void AddInternal(GameObject obj)
    {
        ObjectPoolEntry pooled;

        if (!entries.TryGetValue(obj.name, out pooled))
        {
            Debug.LogError("Trying to add non-pooled object '" + obj.name + "' to pool");
            return;
        }

        if (pooled.IsNetworked)
            NetworkServer.UnSpawn(obj);

        obj.SetActive(false);
        obj.transform.parent = transform;

        Queue<GameObject> pool = pooled.Pool;
        pool.Enqueue(obj);
    }

    /// <summary>
    ///     Checks if object pool has this specific object pooled.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool Contains(GameObject obj)
    {
        return instance.ContainsInternal(obj);
    }

    public static bool Contains(Component obj)
    {
        return instance.ContainsInternal(obj.gameObject);
    }

    private bool ContainsInternal(GameObject obj)
    {
        return entries.ContainsKey(obj.name);
    }

}