using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProceduralTreeSpawner : MonoBehaviour
{
    /// <summary>
    /// Procedural Tree spawner 
    /// </summary>
    
    [SerializeField] private float SpawnX = 100.0f;//width of the plane
    [SerializeField] private float SpawnZ = 100.0f;//depth of the plane

    [SerializeField] private GameObject[] _treesToSpawn;

    [SerializeField] Transform _enemyTransform;
    [SerializeField] Transform _playerTransform;
    private List<GameObject> Trees;


    private void Start()
    {      
        GenerateTrees();    
    }

    private void GenerateTrees()
    {
        Trees = new List<GameObject>();
        for (int x = 0; x < SpawnX; x+=5)
        {
            for (int z = 0; z < SpawnZ; z+=5)
            {
                if (Random.value > 0.7f)//should we place a tree?
                {
                    Vector3 pos = new Vector3(x - SpawnX / 2.0f, 2.2f, z - SpawnZ / 2.0f);
                    int randomIndex = Random.Range(0, _treesToSpawn.Length - 1);
                    
                    var trees = Instantiate(_treesToSpawn[randomIndex], pos, Quaternion.identity, transform);
                    Instantiate(trees);       
                    Trees.Add(trees);
                }
            }
        }

       // InRange();     
    }

    //check if the trees are in range of the player / enemy on spawn.
    // if they are, then they should not spawn
    //private void InRange()//not working
    //{  
    //    float range = 10.0f;

    //    List<Transform> TreesInRange = new List<Transform>();

    //    foreach (var item in Trees)
    //    {
    //        if (Vector3.Distance(_playerTransform.position, item.transform.position) < range)
    //        {
    //            TreesInRange.Add(item.transform);
    //        }
    //            Destroy(Trees[TreesInRange.Count].gameObject);

    //            Trees.RemoveAt(Trees.Count - 1);

    //    }       
    //}
}
