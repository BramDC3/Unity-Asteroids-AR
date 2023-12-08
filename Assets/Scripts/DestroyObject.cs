using UnityEngine;
using UnityEngine.Events;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.1f;
    [SerializeField] private UnityEvent onDestroy;
    
    public void DestroyWithDelay()
    {
        Debug.Log($"{gameObject.name} destroyed");
        onDestroy?.Invoke();
        Destroy(gameObject, destroyDelay);
    }
}
