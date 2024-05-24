using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class menu_Item_Spawner : MonoBehaviour

{
    public GameObject[] prefabs; 
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f; 
    public Canvas canvas; 

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(SpawnItemAtIntervals());
    }

    private IEnumerator SpawnItemAtIntervals()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
            InstantiatePrefabAtRandomPosition();
        }
    }

    private void InstantiatePrefabAtRandomPosition()
    {
        if (boxCollider != null && prefabs.Length > 0)
        {
            Bounds bounds = boxCollider.bounds;
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            // Select a random prefab from the array
            GameObject prefabToInstantiate = prefabs[Random.Range(0, prefabs.Length)];

            // Instantiate the prefab at the random position within the Canvas
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity, canvas.transform);

        
        }
        else
        {
          
        }
    }
}