using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 5;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
