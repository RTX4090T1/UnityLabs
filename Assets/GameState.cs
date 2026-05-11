using System;
using System.IO;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("Lives")]
    public int maxLives = 3;
    public int lives;

    [Header("Stats")]
    public int collisions;
    public int coinsCollected;
    public float levelTime;
    public float timeLimit = 12f;

    [Header("Record")]
    public int bestCoins;
    public float bestTime;

    public event Action OnLose;

    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
        ResetRun();
    }

    void Update()
    {
        levelTime += Time.deltaTime;

        if (levelTime >= timeLimit)
        {
            Lose("Час вийшов");
        }
    }

    public void ResetRun()
    {
        lives = maxLives;
        collisions = 0;
        coinsCollected = 0;
        levelTime = 0f;
    }

    public void AddCoin()
    {
        coinsCollected++;
    }

    public void HitTrap()
    {
        collisions++;
        lives--;

        if (lives <= 0)
        {
            Lose("Життя закінчились");
        }
    }

    private void Lose(string reason)
    {
        Debug.Log($"Програш: {reason}");
        OnLose?.Invoke();
        UpdateRecord();
    }

    private void UpdateRecord()
    {
        if (coinsCollected > bestCoins ||
            (coinsCollected == bestCoins && levelTime < bestTime))
        {
            bestCoins = coinsCollected;
            bestTime = levelTime;
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        var data = new SaveData
        {
            bestCoins = bestCoins,
            bestTime = bestTime
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public void Load()
    {
        if (!File.Exists(SavePath)) return;

        string json = File.ReadAllText(SavePath);
        var data = JsonUtility.FromJson<SaveData>(json);

        bestCoins = data.bestCoins;
        bestTime = data.bestTime;
    }

    [Serializable]
    public class SaveData
    {
        public int bestCoins;
        public float bestTime;
    }
}