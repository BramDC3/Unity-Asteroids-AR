using Random = UnityEngine.Random;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToCreate;
    [SerializeField] private bool useSpawnPoint;
    [Range(0,1)][SerializeField] private float chance = 1;
    [SerializeField] private Transform referenceObjectRotation;
    [SerializeField] private int amountOfObjects = 1;
    [SerializeField] private bool randomizeInitialPosition;
    [SerializeField] private float xRandomizationFactor = 1;
    [SerializeField] private float yRandomizationFactor = 1;
    [SerializeField] private float zRandomizationFactor = 1;
    
    private Transform _spawnPoint;
    private Quaternion _objectRotation;
    
    public void CreateNewObject()
    {
        for (var i = 0; i < amountOfObjects; i++)
        {
            if (Random.value < chance)
            {
                _objectRotation = referenceObjectRotation == null ? Quaternion.identity : referenceObjectRotation.rotation;
                if (useSpawnPoint)
                {
                    var clone = Instantiate(objectToCreate, _spawnPoint.position, _objectRotation);
                    clone.name = $"{clone.name} {clone.GetInstanceID()}";
                }
                else
                {
                    var spawnPoint = transform.position;
                    if (randomizeInitialPosition)
                    {
                        spawnPoint.x += Random.Range(-xRandomizationFactor, xRandomizationFactor);
                        spawnPoint.y += Random.Range(-yRandomizationFactor, yRandomizationFactor);
                        spawnPoint.z += Random.Range(-zRandomizationFactor, zRandomizationFactor);
                    }
                    var clone = Instantiate(objectToCreate, spawnPoint, _objectRotation);
                    clone.name = $"{clone.name} {clone.GetInstanceID()}";
                }    
            } 
        }
    }
}
