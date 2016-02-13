using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilder : SingletonComponent<WorldBuilder>
{

    [SerializeField]
    private Transform playerVehicle;

    [SerializeField]
    private WorldElement[] worldElements;
    
    [Header("Number of Y grid squares ahead")]
    public int creationDistance = 50;
    [Header("Number of Y grid squares behind")]
    public int cleanupDistance = 60;
    
    private WorldElement lastChosenElement;
    private WorldElement secondLastChosenElement;

    private List<WorldElement> activeElements;
    private int gridDistance = 0;

    void Start()
    {
        if (instance != this)
            Destroy(gameObject);

        activeElements = new List<WorldElement>(GetComponentsInChildren<WorldElement>());
        activeElements.Sort((w1, w2) => w1.transform.position.z > w2.transform.position.z ? 1
                    : (w1.transform.position.z < w2.transform.position.z ? -1 : 0));

        foreach (WorldElement element in activeElements)
        {
            element.gridX = gridDistance;
            element.gridY = 0;
            gridDistance += element.length;
            element.ActivateObjects();
        }
        
        InvokeRepeating("WorldCleanup", 10, 10);
    }

    void Update()
    {
        if (playerVehicle.position.x > gridDistance - creationDistance)
            SpawnElement();
    }

    void WorldCleanup()
    {
        if (activeElements.Count == 0)
            return;

        WorldElement element;
        for (int i = activeElements.Count - 1; i >= 0; i--)
        {
            element = activeElements[i];
            if (element.gridX < playerVehicle.position.x - cleanupDistance)
            {
                activeElements.RemoveAt(i);
                Destroy(element.gameObject);
            }
        }

    }
    
    void SpawnElement()
    {
        if (worldElements.Length == 0)
            return;
        
        List<WorldElement> potentialElements = new List<WorldElement>();
        foreach (WorldElement el in worldElements)
            potentialElements.Add(el);

        if (potentialElements.Count == 0)
            return;

        // make sure we don't spawn the same element 3 times in a row
        WorldElement chosenElement = null;
        int attempts = 10;
        while (attempts > 0 && (chosenElement == null || (chosenElement == lastChosenElement && chosenElement == secondLastChosenElement)))
        {
            chosenElement = potentialElements[Random.Range(0, potentialElements.Count)];
            attempts--;
        }

        secondLastChosenElement = lastChosenElement;
        lastChosenElement = chosenElement;

        //Debug.Log("Spawning " + chosenElement.name + " (" + chosenElement.Difficulty + ") adding " + chosenElement.Width + " to grid distance " + gridDistance + " with difficulty " + spawnDifficulty);

        GameObject newElement = (GameObject)Instantiate(chosenElement.gameObject, transform.position + Vector3.right * gridDistance, chosenElement.transform.rotation);
        WorldElement nElement = newElement.GetComponent<WorldElement>();
        nElement.gridX = gridDistance;
        nElement.gridY = 0;
        nElement.transform.parent = transform;

        gridDistance += chosenElement.length;
        activeElements.Add(nElement);
    }
    
}
