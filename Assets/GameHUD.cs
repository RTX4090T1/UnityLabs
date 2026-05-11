using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public Text hpText;
    public Text timeText;

    void Update()
    {
        if (GameState.Instance == null) return;
        if (hpText == null || timeText == null) return;

        hpText.text = $"HP: {GameState.Instance.lives}";
        timeText.text = $"Time: {GameState.Instance.levelTime:F1}/{GameState.Instance.timeLimit:F0}";
    }
}