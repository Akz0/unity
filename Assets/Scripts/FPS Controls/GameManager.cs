using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Area
{
    CLOSE,
    LEFT,
    RIGHT,
    MIDDLE,
    FAR
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private Area ShootingArea;

    public float gameStartTime;
    public float gameEndTime;

    [HideInInspector]
    public int score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI areaText;

    public void StartGame() { }
    public void EndGame() { }

    private void Awake()
    {
        CreateManager();
        gameStartTime = Time.time;
        scoreText = GameObject.Find("ui_Score").GetComponent<TextMeshProUGUI>();
        areaText = GameObject.Find("ui_Area").GetComponent<TextMeshProUGUI>();
    }

    private void CreateManager()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void UpdateScore(int points = 0)
    {
        int totalPoints = points + (int)ShootingArea + 1;
        GameManager.Instance.score += totalPoints;
        Debug.Log("The Score Is: " + GameManager.Instance.score);
        scoreText.text = GameManager.Instance.score.ToString();
    }

    public void SetShootingArea(Area area)
    {
        ShootingArea = area;
        areaText.text = GameManager.Instance.ShootingArea.ToString();
    }
}
