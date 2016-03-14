﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    TextAsset xmlDoc;
    XmlParser xmlParser;

    public GlobalVariables.TypeOfSudokuMatrix matrixType;

    private int matrixSize;
    private int[,] matrix;
    private int[,] matrixComplete;
    public List<Image> imageList;
    public List<GameObject> fishPrefabs;
    public List<GameObject> fishDragAndDropPrefabs;
    public Canvas canvas;

    private int attemptsCount;

    void Start()
    {
        matrixSize = (int)matrixType;
        matrix = new int[matrixSize, matrixSize];
        matrixComplete = new int[matrixSize, matrixSize];

        ParseXml(1);

        switch (matrixType)
        {
            case GlobalVariables.TypeOfSudokuMatrix.Four:
                attemptsCount = 1;
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Five:
                attemptsCount = 1;
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Six:
                attemptsCount = 1;
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Seven:
                attemptsCount = 2;
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Eight:
                attemptsCount = 2;
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Nine:
                attemptsCount = 2;
                break;
        }
    }

    private void ParseXml(int _levelID)
    {
        xmlDoc = (TextAsset)Resources.Load("LevelsManager");
        xmlParser = XmlParser.LoadFromText(xmlDoc.text);

        foreach (var mm in xmlParser.allLevels)
        {
            if (mm.levelId == _levelID)
            {
                foreach (var nn in mm.elementsMatrix)
                {
                    GenerateMatrix(nn);
                }
            }
        }

        xmlDoc = (TextAsset)Resources.Load("LevelsComplete");
        xmlParser = XmlParser.LoadFromText(xmlDoc.text);

        int i = 0, j = 0, tempMatrixComplete = 0;
        foreach (var mm in xmlParser.allLevels)
        {
            if (mm.levelId == _levelID)
            {
                foreach (var nn in mm.elementsMatrix)
                {
                    i = tempMatrixComplete / matrixSize;
                    j = tempMatrixComplete % matrixSize;
                    matrixComplete[i, j] = nn;

                    tempMatrixComplete++;
                }
            }
        }

        ShowMatrix(matrixComplete);
    }

    private int rows = 0, cols = 0, tempCount = 0;
    private ListElement tempImageMatrix;
    private void GenerateMatrix(int _value)
    {
        matrix[rows, cols] = _value;
        tempCount = rows * matrixSize + cols;

        tempImageMatrix = imageList[tempCount].GetComponentInChildren<ListElement>();

        if (_value != 0)
        {
            tempImageMatrix.isAnchor = true;
            GameObject go = Instantiate(fishPrefabs[_value-1]);
            go.transform.SetParent(tempImageMatrix.gameObject.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(5, 10); // right - top
            go.GetComponent<RectTransform>().offsetMax = new Vector2(-5, 0); // left - bottom
        }

        cols++;
        if (cols == 4)
        {
            rows++;
            cols = 0;
        }
    }

    private int fishId;
    public void FishButton(int _fishId)
    {
        fishId = _fishId;
        GameObject go = Instantiate(fishDragAndDropPrefabs[_fishId-1],Input.mousePosition,Quaternion.identity) as GameObject;
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
    }

    public void DropSomeFishElement(Transform _thisSlot)
    {
        if (_thisSlot.GetComponent<ListElement>().isAnchor == false)
        {
            if (_thisSlot.childCount > 0)
                Destroy(_thisSlot.GetChild(0).gameObject);

            GameObject go = Instantiate(fishPrefabs[DragAndDrop.id - 1]);
            go.transform.SetParent(_thisSlot);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(5, 10); // right - top
            go.GetComponent<RectTransform>().offsetMax = new Vector2(-5, 0); // left - bottom

            int i, j, tempPosInArr;
            tempPosInArr = int.Parse(_thisSlot.name);
            i = tempPosInArr / matrixSize;
            j = tempPosInArr % matrixSize;

            matrix[i, j] = fishId;
        }

        if (MatrixIsFull())
            CheckSudoku();

        ShowMatrix(matrix);
    }

    public bool MatrixIsFull()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                if (matrix[i, j] == 0)
                    return false;
            }
        }

        return true;
    }

    //Temp Show Matrix
    public Text tempTextMatrixShow;
    public void ShowMatrix(int[,] _tempMatrix)
    {
        tempTextMatrixShow.text = "";
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                tempTextMatrixShow.text += _tempMatrix[i, j].ToString() + " ";
            }
            tempTextMatrixShow.text += "\n";
        }
    }

    public void CheckSudoku()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                if(matrix[i,j] != matrixComplete[i,j])
                {
                    if (attemptsCount <= 0)
                    {
                        Debug.Log("You Lose One Life :(");
                    }
                    else
                    {
                        attemptsCount--;
                        Debug.Log("Attempts: " + attemptsCount);
                    }
                    return;
                }
            }
        }
        Debug.Log("You Win!!!");
    }
}
