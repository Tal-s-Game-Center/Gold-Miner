using UnityEngine;
using System.Collections.Generic;

public class RandomSpawner2D : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("The prefab to spawn.")]
    public GameObject prefabToSpawn;

    [Tooltip("Number of objects to spawn.")]
    public int numberOfCopies = 10;

    [Tooltip("Minimum and maximum scale for spawned objects.")]
    public float minScale = 0.2f;
    public float maxScale = 3f;

    [Header("Spawn Area")]
    [Tooltip("Minimum and maximum positions for spawning (x, y).")]
    public Vector2 minPosition = new Vector2(-10, -10);
    public Vector2 maxPosition = new Vector2(10, 10);

    [Tooltip("Minimum distance between spawned objects.")]
    public float minDistance = 1f;

    private List<Vector2> usedPositions = new List<Vector2>();

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is not assigned in the Inspector!");
            return;
        }

        int spawnedCount = 0;

        while (spawnedCount < numberOfCopies)
        {
            // Generate random position within the defined area
            Vector2 randomPosition = new Vector2(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y)
            );

            // Check if the position is valid
            if (IsPositionValid(randomPosition))
            {
                // Generate random Z rotation for 2D objects
                float randomRotation = Random.Range(0f, 360f);

                // Generate random scale
                float randScale = Random.Range(minScale, maxScale);
                Vector2 randomScale = new Vector2(randScale, randScale);

                // Instantiate the prefab
                GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.Euler(0, 0, randomRotation));

                // Apply random scale
                spawnedObject.transform.localScale = new Vector3(randomScale.x, randomScale.y, 1);

                // Optional: Parent the object to the spawner for organization
                spawnedObject.transform.parent = this.transform;

                // Add the position to the list of used positions
                usedPositions.Add(randomPosition);

                spawnedCount++;
            }
        }

        Debug.Log($"{spawnedCount} objects spawned successfully!");
    }

    private bool IsPositionValid(Vector2 newPosition)
    {
        foreach (Vector2 usedPosition in usedPositions)
        {
            // Check the distance between the new position and each used position
            if (Vector2.Distance(newPosition, usedPosition) < minDistance)
            {
                return false; // Position is too close to an existing object
            }
        }
        return true; // Position is valid
    }
}
