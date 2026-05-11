using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DebugTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"DebugTrigger on {name}: entered by {other.name}, tag={other.tag}");
    }
}