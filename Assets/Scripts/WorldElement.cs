using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldElement : MonoBehaviour
{

    public int length;
    public int gridX;
    public int gridY;

    private bool objectsActive;
    private List<GameObject> objects;

    void Awake()
    {
        objects = new List<GameObject>();
        //foreach (var com in GetComponentsInChildren<SpawnerScript>())
        //    objects.Add(com.gameObject);

        objectsActive = true;
        DeactivateObjects();
    }
    
    public void DeactivateObjects()
    {
        if (!objectsActive)
            return;

        objectsActive = false;
        foreach (GameObject obj in objects)
            if (obj != null)
                obj.SetActive(false);
    }

    public void ActivateObjects()
    {
        if (objectsActive)
            return;

        objectsActive = true;
        foreach (GameObject obj in objects)
            if (obj != null)
                obj.SetActive(true);
    }

}
