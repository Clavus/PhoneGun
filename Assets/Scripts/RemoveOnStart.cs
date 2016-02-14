using UnityEngine;
using System.Collections;

public class RemoveOnStart : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject);
    }
}
