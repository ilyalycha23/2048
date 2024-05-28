using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CellAnimation : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI points;

    private float moveTime = .1f; //длительность перемещения
    private float appearTime = .1f; //длительность появления

    private Sequence sequence; //очередь

    public void Move(Cell from, Cell to, bool isMerging) { //анимация перемещения плитки из ячийки в ячейку
        from.CancelAnimation(); //из какой ячейки
        to.SetAnimation(this); //в какую ячейку перемещаемся

        image.color = ColorManager.Instance.CellColors[from.Value];
        points.text = from.Points.ToString();
        points.color = from.Value <= 2 ? 
            ColorManager.Instance.PointsDarkColor : 
            ColorManager.Instance.PointsLightColor;
        //выше делаем ячейку полностью одинаковой той, для которой будет работать анимация

        transform.position = from.transform.position; //перемещаемся в ячейку, из которой мы перемещаемся

        sequence = DOTween.Sequence(); //включаем очередь

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad)); //добовляем в очередь позицию, в которую 

        if (isMerging) { //если объеденяемся
            sequence.AppendCallback(() =>
            {
                image.color = ColorManager.Instance.CellColors[to.Value]; //задаем значение плитки перемещаемой
                points.text = to.Points.ToString();
                points.color = to.Value <= 2 ? 
                    ColorManager.Instance.PointsDarkColor : 
                    ColorManager.Instance.PointsLightColor; //устанавливаем цвет данного значения
            });

            sequence.Append(transform.DOScale(1.2f, appearTime)); //анимация увеличения плитки
            sequence.Append(transform.DOScale(1, appearTime)); //возврат обычного размера
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCell(); //отображаем новое значение на плитке, с которой мы соеденились
            Destroy(); //уничтожаем плитку для анимации
        });
    }

    public void Appear(Cell cell) { //анимация появления рандомной плитки
        cell.CancelAnimation();
        cell.SetAnimation(this);

        image.color = ColorManager.Instance.CellColors[cell.Value];
        points.text = cell.Points.ToString();
        points.color = cell.Value <= 2 ?
            ColorManager.Instance.PointsDarkColor :
            ColorManager.Instance.PointsLightColor;
        //все аналагично тому, что сверху 

        transform.position = cell.transform.position; //позиция точно такая же, как у ячейки
        transform.localScale = Vector2.zero; //размер плитки нулевой, т.к. плитка появляется

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, appearTime * 2));
        sequence.Append(transform.DOScale(1f, appearTime * 2));
        sequence.AppendCallback(() =>
        {
            cell.UpdateCell();
            Destroy();
        });
    }

    public void Destroy() { 
        sequence.Kill(); //остановка анимации
        Destroy(gameObject); //уничтожение объекта
    }
}
