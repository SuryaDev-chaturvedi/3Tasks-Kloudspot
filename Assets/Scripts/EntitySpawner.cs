using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityPrefab; // Assign your entity prefab in the Unity Inspector
    public Transform spawnContainer; // Assign the empty GameObject container in the Unity Inspector

    [Header("X and Z Position")]
    public Vector2[] spawnPositions; // Your 2D array of positions

    void Start()
    {
        SpawnEntities();
    }

    IEnumerator delay1sec()
    {
        yield return new WaitForSeconds(1);
    }

    void SpawnEntities()
    {
        foreach (Vector2 spawnPosition in spawnPositions)
        {
            StartCoroutine(delay1sec());

            // Instantiate the entity prefab at the specified position (x, z) with a default y position (e.g., 0).
            Vector3 spawnPosition3D = new Vector3(spawnPosition.x, 0f, spawnPosition.y);
            GameObject spawnedEntity = Instantiate(entityPrefab, spawnPosition3D, Quaternion.identity);

            // Parent the spawned entity to the spawnContainer for organization.
            spawnedEntity.transform.parent = spawnContainer;
        }
    }
}
