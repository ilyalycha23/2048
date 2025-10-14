# 🧩 2048 Game

[![Unity](https://img.shields.io/badge/Engine-Unity-000000.svg?style=flat&logo=unity)](https://unity.com/)
[![C#](https://img.shields.io/badge/Language-C%23-239120.svg?style=flat&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![GitHub](https://img.shields.io/badge/IDE-Visual%20Studio-5C2D91.svg?style=flat&logo=visual-studio)](https://visualstudio.microsoft.com/)

**2048** — это увлекательная и захватывающая логическая головоломка, где ваша цель — соединять плитки с одинаковыми числами, чтобы получить заветную плитку **2048** или даже превзойти этот рубеж!
![Обзор проекта](https://github.com/ilyalycha23/2048/blob/main/obzor.jpg)

---

## 🚀 Технологии

В разработке этого проекта были использованы следующие технологии:

*   **Движок:** Unity
*   **Язык программирования:** C#
*   **Среда разработки:** Visual Studio

---

## 📥 Установка и Запуск

Чтобы запустить игру на своем компьютере, выполните следующие шаги:

1.  **Клонируйте репозиторий:**
    ```bash
    git clone https://github.com/ilyalycha23/2048.git
    ```

2.  **Откройте проект** в Unity Hub.

3.  **Соберите проект:**
    *   В меню Unity перейдите `File -> Build Settings`.
    *   Выберите целевую платформу (Windows, MacOS, WebGL и т.д.).
    *   Нажмите кнопку **`Build`** и дождитесь создания исполняемого файла.

4.  **Запустите** собранный файл и наслаждайтесь игрой!

---

## ⚙️ Ключевые функции (API)

Основная логика игры реализована в классе клетки (Cell). Вот основные публичные методы:

| Метод | Описание |
| :--- | :--- |
| `SetValue(int value)` | Задает числовое значение и визуал плитки. |
| `IncreaseValue()` | Увеличивает значение плитки (x2) при слиянии двух одинаковых. |
| `ResetFlags()` | Сбрасывает внутренние флаги (например, `HasMerged`) после каждого хода. |
| `MergeWithCell(Cell other)` | Выполняет слияние текущей плитки с другой. |
| `MoveToCell(Cell target)` | Перемещает плитку в целевую (пустую) ячейку. |
| `UpdateCell()` | Обновляет состояние и внешний вид плитки. |
| `SetAnimation()` | Запускает анимацию появления/изменения плитки. |
| `CancelAnimation()` | Останавливает текущую анимацию. |

---

## 👨‍💻 Разработчик

*   **Лаврешин Илья Александрович**

---

## 📧 Контакты

По всем вопросам и предложениям вы можете написать на почту:  
[![Email](https://img.shields.io/badge/Email-ilyalycha23@gmail.com-D14836?style=flat&logo=gmail&logoColor=white)](mailto:ilyalav2323@gmail.com)

---

**Удачи в достижении 2048!** 🎉
