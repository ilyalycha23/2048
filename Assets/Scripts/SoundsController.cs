using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsController : MonoBehaviour
{
    public GameObject Sounds, btn_noSound, btn_Sound;
    public Slider slider;

    public void Update()
    {
        if (slider.value == 0) { //если микшер громкости на нуле
            btn_noSound.SetActive(true); //включаем кнопку "без звука"
            btn_Sound.SetActive(false); //выключаем кнопку "звук"
        }
        if (slider.value != 0) //если микшер громкости больше 0
        {
            btn_Sound.SetActive(true); //включаем кнопку "звук"
            btn_noSound.SetActive(false); //выключаем кнопку "без бвука"
        }
    }
    public void SlideBar_Null() { //при нажатии кнопки "звук"
        btn_Sound.SetActive(false); //кнопка "звук" выключается
        btn_noSound.SetActive(true); //кнопка "без звука включается"
        slider.value = 0; //значение микшера присваевается 0
    }

    public void SlideBar_On() { //при нажатии кнопки "без звука"
        btn_Sound.SetActive(true); //кнопка "звук" включается
        btn_noSound.SetActive(false); //кнопка "без звука" выключается
        slider.value = 1; //микшер громкости на макс.
    }
}
