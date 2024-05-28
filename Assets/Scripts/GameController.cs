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
        gameResult.text = ""; //������� �����������
        LastScoreText.text = ""; //������� ��������� ����

        SetPoints(0); //�������� ����
        GameStarted = true;

        Field.Instance.GenerateField(); //������� ����
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "�� �������!";
    }

    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "�� ��������!";
    }

    public void AddPoints(int points) {
        SetPoints(Points + points); //���������� �����
    }

    public void SetPoints(int points) {
        Points = points;
        pointsText.text = Points.ToString(); //����� ����� �� �����
        LastScoreText.text = Points.ToString(); //����� ����� �� ��������� ���� �� �����
    }

    public TextMeshProUGUI BestScore;
    public int BestPoints;
    public void Update()
    {
    //����� ������� ���������� ����� � ������� ����
        BestScore.text = "" + PlayerPrefs.GetInt("HighScore");
        BestPoints = Points;
        if (PlayerPrefs.GetInt("HighScore") <= BestPoints)
            PlayerPrefs.SetInt("HighScore", BestPoints);

    }
}
