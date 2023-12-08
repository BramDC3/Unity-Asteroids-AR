using System.Collections;
using UnityEngine;

public class SpawnColliderArea : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private new Collider collider;
    [SerializeField] private float spawnDelayTime = 1;
    [SerializeField] private GameState gameState;
    private Vector3 _spawnPosition;

    private void Start()
    {
        // start spawn
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // continue loop while GameOver is false
        while (!gameState.GameOver)
        {
            // get new position for asteroid
            _spawnPosition = GetSpawnPosition();

            // create a new asteroid from prefab
            var clone = Instantiate(prefab, _spawnPosition, Quaternion.identity);

            // change new asteroid's game object name
            clone.name = $"{clone.name}{clone.GetInstanceID()}";

            // wait before looping again
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        // get a random position from the bounds of the AsteroidSpawner's Box Collider
        // use this to position our new spawned asteroid
        var bounds = collider.bounds;
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(collider.bounds.min.y, bounds.max.y),
            Random.Range(collider.bounds.min.z, bounds.max.z));
    }  
}
