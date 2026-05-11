using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeroRespawn6 : MonoBehaviour
{
    public Vector3 startPosition;
    public Rigidbody heroRigidbody;

    void Start()
    {
        if (heroRigidbody == null)
            heroRigidbody = GetComponent<Rigidbody>();

        startPosition = transform.position;

        Debug.Log($"Respawn start position = {startPosition}");
    }

    public void Respawn()
    {
        Debug.Log("RESPAWN called");

        transform.position = startPosition;

        if (heroRigidbody != null)
        {
            heroRigidbody.linearVelocity = Vector3.zero;
            heroRigidbody.angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter with {other.name}, tag={other.tag}");

        if (other.CompareTag("Pit"))
        {
            Respawn();
        }
    }
}