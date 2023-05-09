using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomObject : MonoBehaviour
{
    /// <summary>
    /// Script to select a random object to spawn for the procedural trees
    /// </summary>

    public GameObject[] objectsToPickFrom;

    void Start()
    {
        PickObject(); 
    }

    private void PickObject()
    {
        int randomIndex = Random.Range(0, objectsToPickFrom.Length -1);
        GameObject clone = Instantiate(objectsToPickFrom[randomIndex], transform.position, Quaternion.identity);
    }

 
}
