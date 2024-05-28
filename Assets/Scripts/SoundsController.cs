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
        if (slider.value == 0) { //���� ������ ��������� �� ����
            btn_noSound.SetActive(true); //�������� ������ "��� �����"
            btn_Sound.SetActive(false); //��������� ������ "����"
        }
        if (slider.value != 0) //���� ������ ��������� ������ 0
        {
            btn_Sound.SetActive(true); //�������� ������ "����"
            btn_noSound.SetActive(false); //��������� ������ "��� �����"
        }
    }
    public void SlideBar_Null() { //��� ������� ������ "����"
        btn_Sound.SetActive(false); //������ "����" �����������
        btn_noSound.SetActive(true); //������ "��� ����� ����������"
        slider.value = 0; //�������� ������� ������������� 0
    }

    public void SlideBar_On() { //��� ������� ������ "��� �����"
        btn_Sound.SetActive(true); //������ "����" ����������
        btn_noSound.SetActive(false); //������ "��� �����" �����������
        slider.value = 1; //������ ��������� �� ����.
    }
}
