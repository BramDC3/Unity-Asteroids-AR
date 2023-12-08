using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UFOAttack : MonoBehaviour
{
    [SerializeField] private float fireCooldownTime = 0.3f;
    [SerializeField] private UnityEvent onShoot;
    
    // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        // continue loop while the UFOAttack component is enabled.
        while (enabled)
        {
            // trigger shooting event 
            onShoot?.Invoke();
            
            // wait before looping again
            yield return new WaitForSeconds(fireCooldownTime);
        }
    }
}
