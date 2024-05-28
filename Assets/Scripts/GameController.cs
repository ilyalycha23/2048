using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    public TextMeshProUGUI gameResult;
    public TextMeshProUGUI pointsText;

    public TextMeshProUGUI LastScoreText;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartGame() {
        gameResult.text = ""; //очистка результатов
        LastScoreText.text = ""; //очистка последней игры

        SetPoints(0); //обнуляем очки
        GameStarted = true;

        Field.Instance.GenerateField(); //создаем поле
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "Ты выиграл!";
    }

    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "Ты проиграл!";
    }

    public void AddPoints(int points) {
        SetPoints(Points + points); //добавление очков
    }

    public void SetPoints(int points) {
        Points = points;
        pointsText.text = Points.ToString(); //вывод очков на экран
        LastScoreText.text = Points.ToString(); //вывод очков за последнюю игру на экран
    }

    public TextMeshProUGUI BestScore;
    public int BestPoints;
    public void Update()
    {
    //вывод лучшего количества очков в главном меню
        BestScore.text = "" + PlayerPrefs.GetInt("HighScore");
        BestPoints = Points;
        if (PlayerPrefs.GetInt("HighScore") <= BestPoints)
            PlayerPrefs.SetInt("HighScore", BestPoints);

    }
}
