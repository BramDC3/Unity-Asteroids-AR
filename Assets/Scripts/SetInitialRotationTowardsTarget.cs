using UnityEngine;

public class SetInitialRotationTowardsTarget : MonoBehaviour
{
    [SerializeField] private string targetName;
    [SerializeField] private Vector3 randomOffset;

    private void Start()
    {
        // look for the target in the hierarchy
        var targetObject = GameObject.Find(targetName);

        // make sure the target exists, if not return and do nothing
        if (targetObject == null) return;

        // get the target's position to get the direction
        var direction = targetObject.transform.position - transform.position;

        // randomize based on the offset and add to the direction
        direction += new Vector3(Random.Range(-randomOffset.x, randomOffset.x),
            Random.Range(-randomOffset.y, randomOffset.y),
            Random.Range(-randomOffset.z, randomOffset.z));

        // set the target's initial rotation based on the direction
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
