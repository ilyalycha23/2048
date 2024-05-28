using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public static Field Instance;

    [Header("Field Properties")]
    public float CellSize; //размер плитки
    public float Spacing; //отступы между плитками
    public int FieldSize; //размер поля
    public int InitCellsCount; //кол-во плиток при старте

    [Space(10)]
    [SerializeField]
    private Cell cellPref;
    [SerializeField]
    private RectTransform rt;

    private Cell[,] field; //двумерный массив для хранения всего поля

    private bool anyCellMoved; //перемещалась ли какая-нибудь плитка

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnInput(Vector2 direction) {
        if (!GameController.GameStarted) { //началась ли игра
            return;
        }

        anyCellMoved = false; //плитка переместилась
        ResetCellsFlags(); //обнуление флага (объединялась ли плитка)

        Move(direction); 

        if (anyCellMoved) { //при перемещение
            GenerateRandCell(); //генирируем рандомную плитку
            CheckGameResult(); //проверяем результаты
        }
    }

    private void Move(Vector2 direction)
    {
        int startXY = direction.x > 0 || direction.y < 0 ? FieldSize - 1 : 0; //определяем нахождение плитки по XY
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y; //значение направления хода (верт./гор. ход)

        for (int i = 0; i < FieldSize; i++)
        {
            for (int k = startXY; k >= 0 && k < FieldSize; k -= dir)
            {
                var cell = direction.x != 0 ? field[k, i] : field[i, k]; //в зависимости от направления меняем местами XY

                if (cell.IsEmpty) //пустая ли плитка
                {
                    continue;
                }

                var cellToMerge = FindCellToMerge(cell, direction); //ищем плитку для объединения
                if (cellToMerge != null) { //плитка нашлась
                    cell.MergeWithCell(cellToMerge); //соединение плиток
                    anyCellMoved = true; 

                    continue;
                }

                var emptyCell = FindEmptyCell(cell, direction); //плитка не нашлась, продолжаем поиск в другом направление
                if (emptyCell != null)
                {
                    cell.MoveToCell(emptyCell);
                    anyCellMoved = true;
                }
            }
        }
    }

    private Cell FindCellToMerge(Cell cell, Vector2 direction) { 
        int startX = cell.X + (int)direction.x; // определяем X первой и следующей плитки по движению
        int startY = cell.Y - (int)direction.y; // определяем Y первой и следующей плитки по движению

        for (int x = startX, y = startY;
            x >= 0 && x < FieldSize && y >= 0 && y < FieldSize;
            x += (int)direction.x, y -= (int)direction.y) //проверяем, не вылезли ли мы за пределы плитки
        {
            if (field[x, y].IsEmpty) // пустая ли плитка
                continue;

            if (field[x, y].Value == cell.Value && !field[x, y].HasMerged) { //не пустая, совпадают номиналы или нет и объеденялась ли плитка
                return field[x, y]; //возвращаем плитку для объединения
            }

            break;
        }
        return null; // объединиться не с кем 
    }

    private Cell FindEmptyCell(Cell cell, Vector2 direction) { //поиск пустой плитки
        Cell emptyCell = null;

        int startX = cell.X + (int)direction.x; // определяем X первой и следующей плитки по движению
        int startY = cell.Y - (int)direction.y; // определяем Y первой и следующей плитки по движению

        for (int x = startX, y = startY;
            x >= 0 && x < FieldSize && y >= 0 && y < FieldSize;
            x += (int)direction.x, y -= (int)direction.y)//проверяем, не вылезли ли мы за пределы плитки
        {
            if (field[x, y].IsEmpty) //пустая ли плитка
                emptyCell = field[x, y];
            else
                break; //плитка преграждает путь
        }
        return emptyCell; //возвращаем плитку для объединения
    }

    private void CheckGameResult() {
        bool lose = true;

        for (int x = 0; x  < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                if (field[x, y].Value == Cell.MaxValue) { //значение плитки достигло максимального
                    GameController.Instance.Win();
                    return;
                }

                if (lose && field[x, y].IsEmpty ||
                    FindCellToMerge(field[x,y], Vector2.left) ||
                    FindCellToMerge(field[x, y], Vector2.right) ||
                    FindCellToMerge(field[x, y], Vector2.up) ||
                    FindCellToMerge(field[x, y], Vector2.down)) // пустая плитка, либо может объедениться
                { 
                    lose = false; //не проиграли
                }
            }
        }
        if (lose)
            GameController.Instance.Lose();
    }

    private void Start()
    {
        GenerateField();
        SwipeDetection.SwipeEvent += OnInput; //внедрение свайпа в игру
    }

    private void CreateField()
    {
        field = new Cell[FieldSize, FieldSize]; //инициализация массива поля

        float fieldWidth = FieldSize * (CellSize + Spacing) + Spacing; //задаём размер поля
        rt.sizeDelta = new Vector2(fieldWidth, fieldWidth); //размер объекта на канвасе 

        float startx = -(fieldWidth / 2) + (CellSize / 2) + Spacing; //начальная позиция для 1-ой плитки по X 
        float startY = (fieldWidth / 2) - (CellSize / 2) - Spacing; //начальная позиция для 1-ой плитки по Y 

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false); //создание объекта в месте transform
                var position = new Vector2(startx + (x * (CellSize + Spacing)), startY - (y * (CellSize + Spacing))); //позиция плитки
                cell.transform.localPosition = position;

                field[x, y] = cell; //добавляем в массив плиток

                cell.SetValue(x, y, 0); //делаем значение для плиток = 0 
            }
        }
    }

    public void GenerateField()
    {
        if (field == null)
        {
            CreateField();
        }

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                field[x, y].SetValue(x, y, 0); //установка значения 0 для каждой плитки
            }
        }

        for (int i = 0; i < InitCellsCount; i++)
        {
            GenerateRandCell(); //генерируем указанное число рандомных плиток с изначальным значением
        }
    }

    public void GenerateRandCell()
    {
        var empyCells = new List<Cell>();

        for (int x = 0; x < FieldSize; x++) {
            for (int y = 0; y < FieldSize; y++) {
                if (field[x, y].IsEmpty) { 
                    empyCells.Add(field[x,y]);//делаем все плитки пустыми и добавляем их в список
                }
            }
        }

        int value = Random.Range(0, 10) == 0 ? 2 : 1; // 90% - "2", 10% - "4"

        var cell = empyCells[Random.Range(0, empyCells.Count)]; //определяем рандомную плитку
        cell.SetValue(cell.X, cell.Y, value, false); //заносим значение в эту плитку

        CellAnimationController.Instance.SmoothAppear(cell); //анимация плавного появления плитки
    }

    private void ResetCellsFlags() {
        for (int x = 0; x < FieldSize; x++) { 
            for (int y = 0; y < FieldSize; y++) {
                field[x,y].ResetFlags(); //обнуляем флаг (объединялась ли плитка) у всех плиток на поле
            }
        }
    }
}
