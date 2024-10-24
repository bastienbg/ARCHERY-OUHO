using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public GameObject rabbitPrefab; 
    public int numberOfRabbits = 5;  
    public float spawnInterval = 2f; 
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); 

    private void Start()
    {
        
        StartCoroutine(SpawnRabbits());
    }

    private IEnumerator SpawnRabbits()
    {
        for (int i = 0; i < numberOfRabbits; i++)
        {
            SpawnRabbit();
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    private void SpawnRabbit()
    {

        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        Instantiate(rabbitPrefab, transform.position + spawnPosition, Quaternion.identity);
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
