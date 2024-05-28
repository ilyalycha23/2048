using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);

    private Vector2 tapPosition; //позиция во время начального касания
    private Vector2 swipeDelta; //позиция дельты

    private float deadZone = 80; //мин. дистанция свайпа

    private bool isSwiping; //флаг (в процессе свайпа)
    private bool isMobile;

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }
    private void Update()
    {
        if (!isMobile) //если не на телефоне
        {
            if (Input.GetMouseButtonDown(0)) //при нажатие на мышку
            {
                isSwiping = true; 
                tapPosition = Input.mousePosition; //запоминаем позицию
            }
            else if (Input.GetMouseButtonUp(0)) // отжали мышку
            {
                ResetSwipe(); //обнуляем
            }
        }
        else { //на телефоне
            if (Input.touchCount > 0) { //если коснулся экрана
                if (Input.GetTouch(0).phase == TouchPhase.Began) { //берём первое касание, которое только началось
                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position; //запоминаем позицию
                }
                else if (
                    Input.GetTouch(0).phase == TouchPhase.Canceled || 
                    Input.GetTouch(0).phase == TouchPhase.Ended)  //свайп закончился
                {
                    ResetSwipe(); //обнуляем
                }
            }
        }
        CheckSwipe();
    }
    private void CheckSwipe() { 
        swipeDelta = Vector2.zero;

        if (isSwiping) {
            if (!isMobile && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - tapPosition; //высчитываем дельту
            else if (Input.touchCount > 0)
                swipeDelta = Input.GetTouch(0).position - tapPosition;//высчитываем дельту
        }

        if (swipeDelta.magnitude > deadZone) { //длина вектора соответствует мин. длине свайпа
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                SwipeEvent?.Invoke(swipeDelta.x > 0 ? Vector2.right : Vector2.left);//в зависиомсти от направления ветора передаем в него сторону свайпа
            else
                SwipeEvent?.Invoke(swipeDelta.y > 0 ? Vector2.up : Vector2.down);

            ResetSwipe(); //обнуляем переменные после свайпа
        }
    }

    private void ResetSwipe() { //обнуление переменных
        isSwiping = false;

        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
}
