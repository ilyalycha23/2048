using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X { get; private set; } //горизонталь
    public int Y { get; private set; } //вертикаль

    public int Value { get; private set; } //степень номинала плитки
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value); //обычный номинал плитки

    public bool IsEmpty => Value == 0; //флаг (пустая ли плитка)
    public bool HasMerged { get; private set; } //флаг (объединялась ли плитка)

    public const int MaxValue = 11; //макс. степень двойки, 2^11 = 2048

    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI points;

    private CellAnimation currentAnimation; //остановка анимации

    public void SetValue(int x, int y, int value, bool updateUI = true) { //задаем значение для плитки
        X = x;
        Y = y;
        Value = value;

        if (updateUI) 
            UpdateCell(); //новое значение для плитки без видимости
    }

    public void IncreaseValue()  //обновляем значения при объединение плиток
    {
        Value++;
        HasMerged = true;

        GameController.Instance.AddPoints(Points);
    }

    public void ResetFlags() {
        HasMerged = false;    
    }

    public void MergeWithCell(Cell otherCell) { //плитка вливается в плитку

        CellAnimationController.Instance.SmoothTransition(this, otherCell, true); //анимация перемещения

        otherCell.IncreaseValue();//значение плитки, в которую вошли, удвоется
        SetValue(X, Y, 0); //предыдущей плитки устанавливаем значение 0
    }

    public void MoveToCell(Cell target) { //перемещение плитки в пустую клетку
        CellAnimationController.Instance.SmoothTransition(this, target, false); //анимация перемещения

        target.SetValue(target.X, target.Y, Value, false); //задаем значение плитки, в которую переместились
        SetValue(X, Y, 0); //обнуляем значение плитки, из которой мы переместились
    }
    public void UpdateCell() { 
        points.text = IsEmpty ? string.Empty : Points.ToString(); //пустая = 0 очков, !пустая = пишем значение на плитке

        points.color = Value <= 2 ? ColorManager.Instance.PointsDarkColor : 
            ColorManager.Instance.PointsLightColor; // в зависимости от номинала задаем цвет текста

        image.color = ColorManager.Instance.CellColors[Value]; //цвет плитки, зависящий от ее номинала
    }

    public void SetAnimation(CellAnimation animation) {
        currentAnimation = animation; 
    }

    public void CancelAnimation() { 
        if(currentAnimation != null) //анимация прошла
            currentAnimation.Destroy();  //останавливаем анимацию и удаляем объект 
    }
}
