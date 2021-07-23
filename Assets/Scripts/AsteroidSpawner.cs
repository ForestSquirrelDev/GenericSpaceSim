using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Tooltip("Number of asteroids to spawn.")]
    [SerializeField] private int asteroidCount = 10;
    [SerializeField] private GameObject[] prefabs;

    [Tooltip("Size of spawn area. Random.insideUnitSphere is used for spawning.")]
    [SerializeField] private float range = 4000.0f;

    [Tooltip("Minimal scale of prefab. Size will be randomed.")]
    [SerializeField] private float minAsteroidScale = 1.0f;
    [Tooltip("Max scale of prefab.")]
    [SerializeField] private float maxAsteroidScale = 1.0f;
    
    private void Start() => SpawnAsteroids();

    private void SpawnAsteroids()
    {
        if(prefabs.Length == 0)
        {
            Debug.LogWarning("Can't instantiate asteroids. No prefabs have been set in the inspector.");
            return;
        }

        for(int i = 0; i <= asteroidCount; i++)
        {
            GameObject asteroid = Instantiate(original: prefabs[Random.Range(0, prefabs.Length)],
                                              position: Random.insideUnitSphere * range,
                                              rotation: Quaternion.identity,
                                              parent: this.transform);

            float randomScale = Random.Range(minAsteroidScale, maxAsteroidScale);

            asteroid.transform.localScale = new Vector3(x: randomScale, y: randomScale, z: randomScale);
        }
    }
}