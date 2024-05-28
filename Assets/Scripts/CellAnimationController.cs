using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellAnimationController : MonoBehaviour
{
    public static CellAnimationController Instance { get; private set; }

    [SerializeField]
    private CellAnimation animationPref;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DOTween.Init(); //инициализация плагина для анимаций
    }

    public void SmoothTransition(Cell from, Cell to, bool isMerging) { //анимация перемещения
        Instantiate(animationPref, transform, false).Move(from, to, isMerging);
    }

    public void SmoothAppear(Cell cell) { //анимация появления плитки
        Instantiate(animationPref, transform, false).Appear(cell);
    }
}
