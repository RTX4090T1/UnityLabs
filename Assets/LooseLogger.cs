using UnityEngine;

public class LoseLogger : MonoBehaviour
{
    void Start()
    {
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
        Debug.Log("ГРА ПРОГРАНА: виведено повідомлення з обробника події.");
    }
}