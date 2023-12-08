using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private UnityEvent<int> onReceiveDamage;
    [SerializeField] private UnityEvent onZeroHealth;
    [SerializeField] private UnityEvent<int> onReceiveHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public void ReceiveDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        onReceiveDamage?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            onZeroHealth?.Invoke();
        }
    }

    public void GainHealth(int gainAmount)
    {
        currentHealth += gainAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onReceiveHealth?.Invoke(currentHealth);
    }
}
