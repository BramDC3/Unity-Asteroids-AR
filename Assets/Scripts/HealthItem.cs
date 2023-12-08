using UnityEngine;
using UnityEngine.Events;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private string tagToCompare = "Player";
    [SerializeField] private int healingPower = 10;
    [SerializeField] private UnityEvent onHeal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            if (other.TryGetComponent(out Health health))
            {
                health.GainHealth(healingPower);
                
                onHeal?.Invoke();
            }
        }
    }
}
