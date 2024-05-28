using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    public Color[] CellColors;

    [Space(5)]
    public Color PointsDarkColor; // цвет текста для двойки и четверки
    public Color PointsLightColor; // для всего остального номинала

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        PointsDarkColor.a = 1f;
        PointsLightColor.a = 1f;
    }
}
