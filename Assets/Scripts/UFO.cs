using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum UFOStates
{
    Idle, 
    Attacking
}

public class UFO : MonoBehaviour
{
    [SerializeField] private UFOStates currentState;
    [SerializeField] private List<Vector3> trajectoryVectors = new List<Vector3>();
    [SerializeField] private int trajectoriesPerSpawn = 2;
    [SerializeField] private float spawnDistanceFromPlayer = 20;
    [SerializeField] private float xyOffset = 10;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private int cooldownMinTime = 5;
    [SerializeField] private int cooldownMaxTime = 15;
    [SerializeField] private GameState gameState;
    [SerializeField] private UnityEvent onStartAttacking;
    [SerializeField] private UnityEvent onStopAttacking;
    [SerializeField] private UnityEvent onDie;
    [SerializeField] private AudioSfx ufoOnScene;

    private Transform _player;
    
    public UFOStates CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            
            if (currentState == UFOStates.Attacking)
            {
                onStartAttacking?.Invoke();
            }
            else
            {
                onStopAttacking?.Invoke();
            }
        }
    }

    private void Start() 
    {

        // find the player in the scene.
        var playerObject = GameObject.Find("Player");

        // If the Player is found,then grab its transform object.
        if (playerObject)
        {
            _player = playerObject.transform;
        }

        // Players should start in this state.
        CurrentState = UFOStates.Idle;
    }

    public void StartCooldown()
    {
        if (gameState.GameOver)
        {
            ufoOnScene.StopAudio();
            return;
        }

        StartCoroutine(IdleRoutine());
        
        ufoOnScene.StopAudio();
    }

    private IEnumerator IdleRoutine()
    {
        transform.position = new Vector3(1000, 1000, 1000);
        
        trajectoryVectors.Clear();
        
        yield return new WaitForSeconds(Random.Range(cooldownMinTime, cooldownMaxTime));
        
        CurrentState = UFOStates.Attacking;
    }

    public void StartAttacking()
    {
        // Check if the player is available
        if (_player == null) return;
        
        // Define the new spawn position
        var spawnPosition = GetNewPositionVector();
        transform.position = spawnPosition;
        
        // Define new random trajectory vectors
        for (var i = 0; i < trajectoriesPerSpawn; i++)
        {
            trajectoryVectors.Add(GetNewPositionVector());
        }
        
        ufoOnScene.PlayAudio(gameObject);
        
        StartCoroutine(AttackMovement());
    }

    private Vector3 GetNewPositionVector()
    {
        var x = Random.Range(-xyOffset, xyOffset);
        var y = Random.Range(-xyOffset, xyOffset);
        var z = _player.transform.position.z + spawnDistanceFromPlayer;
        
        return new Vector3(x, y, z);
    }

    private IEnumerator AttackMovement()
    {
        for (var i = 0; i < trajectoryVectors.Count; i++)
        {
            var distance = Vector3.Distance(transform.position, trajectoryVectors[i]);

            while (distance > 0.5f && !gameState.GameOver)
            {
                // wait a frame
                yield return null;
                
                transform.position = Vector3.MoveTowards(transform.position, trajectoryVectors[i], Time.deltaTime * movementSpeed);
                
                distance = Vector3.Distance(transform.position,  trajectoryVectors[i]);
            }
        }
        
        CurrentState = UFOStates.Idle;
    }

    public void Die()
    {
        onDie?.Invoke();
        onStopAttacking?.Invoke();
        
        StopAllCoroutines();
        StartCooldown();
        
        ufoOnScene.StopAudio();
    }
}
