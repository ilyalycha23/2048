using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int Points { get; private set; }

    public TextMeshProUGUI BestScore;
    public void Update()
    {
    //����� ������� � ������� ����
        BestScore.text = "" + PlayerPrefs.GetInt("HighScore");
    }
}
