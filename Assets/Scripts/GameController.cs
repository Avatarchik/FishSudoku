using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    TextAsset xmlDoc;
    XmlParser xmlParser;

    [HideInInspector]
    public int matrixSize = 4;

    private int[,] matrix;
    public List<InputField> inpfMatrix;
    
    void Start()
    {
        matrix = new int[matrixSize, matrixSize];
        ParseXml(1);
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
    }

    private int rows = 0, cols = 0, tempCount = 0;
    private void GenerateMatrix(int _value)
    {
        matrix[rows, cols] = _value;
        tempCount = rows * matrixSize + cols;
        inpfMatrix[tempCount].text = _value.ToString();
        cols++;
        if (cols == 4)
        {
            rows++;
            cols = 0;
        }
    }

    public void InsertValueToMatrix()
    {
        
    }

    public void CheckSudoku()
    {
        
    }
}
