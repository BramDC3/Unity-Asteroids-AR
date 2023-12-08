using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private float fireRate = 0.3f;
    
    private bool _canShoot = true;
    private bool _shoot;

    private void Update()
    {
        _shoot = Input.GetMouseButtonDown(0);
            
        if (_shoot && _canShoot)
        {
            onShoot?.Invoke();
            
            _canShoot = false;
            
            StartCoroutine(EnableShooting());
        }
    }
    
    private IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}
