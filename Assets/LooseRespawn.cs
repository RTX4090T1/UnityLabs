using UnityEngine;

public class RespawnOnLose : MonoBehaviour
{
    public Transform player;
    public Vector3 startPos = new Vector3(89f, 6f, 67f);

    private Rigidbody _rb;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("RespawnOnLose: Player not assigned!");
            return;
        }

        _rb = player.GetComponent<Rigidbody>();

        if (GameState.Instance != null)
        {
            GameState.Instance.OnLose += HandleLose;
        }
    }

    void OnDestroy()
    {
        if (GameState.Instance != null)
        {
            GameState.Instance.OnLose -= HandleLose;
        }
    }

    private void HandleLose()
    {
        player.position = startPos;

        if (_rb != null)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        GameState.Instance.ResetRun();
    }
}