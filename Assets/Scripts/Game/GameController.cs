using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GameController : MonoBehaviour
{
    TextAsset xmlDoc;
    XmlParser xmlParser;

    public GlobalVariables.TypeOfSudokuMatrix matrixType;

    [HideInInspector]
    public int matrixSize;
    public int[,] matrix;
    public int[,] matrixComplete;
    public List<Image> imageList;
    public List<GameObject> fishPrefabs;
    public Canvas canvas;

    private int attemptsCount;

    void Start()
    {
        matrixSize = (int)matrixType;
        matrix = new int[matrixSize, matrixSize];
        matrixComplete = new int[matrixSize, matrixSize];

        ParseXml(PlayerPrefs.GetInt("SelectedLevel"));

        switch (matrixType)
        {
            case GlobalVariables.TypeOfSudokuMatrix.Four:
                attemptsCount = 1;
                attempt1Image.SetActive(true);
                attempt2Image.SetActive(false);
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Five:
                attemptsCount = 1;
                attempt1Image.SetActive(true);
                attempt2Image.SetActive(false);
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Six:
                attemptsCount = 1;
                attempt1Image.SetActive(true);
                attempt2Image.SetActive(false);
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Seven:
                attemptsCount = 2;
                attempt1Image.SetActive(false);
                attempt2Image.SetActive(true);
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Eight:
                attemptsCount = 2;
                attempt1Image.SetActive(false);
                attempt2Image.SetActive(true);
                break;
            case GlobalVariables.TypeOfSudokuMatrix.Nine:
                attemptsCount = 2;
                attempt1Image.SetActive(false);
                attempt2Image.SetActive(true);
                break;
        }

        StartCoroutine(ShowAttemptsWindow());
    }

    public GameObject attwmptsWindow;
    public GameObject attempt1Image;
    public GameObject attempt2Image;
    IEnumerator ShowAttemptsWindow()
    {
        if(attemptsCount == 1)
        {
            attempt1Image.SetActive(true);
            attempt2Image.SetActive(false);
        }
        else if (attemptsCount == 2)
        {
            attempt1Image.SetActive(false);
            attempt2Image.SetActive(true);
        }

        attwmptsWindow.SetActive(true);
        yield return new WaitForSeconds(2f);
        attwmptsWindow.SetActive(false);
    }

    private void ParseXml(int _levelID)
    {
        xmlDoc = (TextAsset)Resources.Load("LevelsManager");
        xmlParser = XmlParser.LoadFromText(xmlDoc.text);

        foreach (var mm in xmlParser.allLevels)
        {
            if(mm.levelSize == matrixSize)
            {
                foreach (var nn in mm.levels)
                {
                    if (nn.levelId == _levelID)
                    {
                        foreach (var rr in nn.elementsMatrix)
                        {
                            GenerateMatrix(rr);
                        }
                    }
                }
            }
        }

        xmlDoc = (TextAsset)Resources.Load("LevelsComplete");
        xmlParser = XmlParser.LoadFromText(xmlDoc.text);

        int i = 0, j = 0, tempMatrixComplete = 0;
        foreach (var mm in xmlParser.allLevels)
        {
            if(mm.levelSize == matrixSize)
            {
                foreach(var nn in mm.levels)
                {
                    if(nn.levelId == _levelID)
                    {
                        foreach(var rr in nn.elementsMatrix)
                        {
                            i = tempMatrixComplete / matrixSize;
                            j = tempMatrixComplete % matrixSize;
                            matrixComplete[i, j] = rr;

                            tempMatrixComplete++;
                        }
                    }
                }
            }
        }

        ShowMatrix(matrixComplete);
    }

    private int rows = 0, cols = 0, tempCount = 0;
    [HideInInspector]
    public ListElement tempImageMatrix;
    private void GenerateMatrix(int _value)
    {
        matrix[rows, cols] = _value;
        tempCount = rows * matrixSize + cols;

        tempImageMatrix = imageList[tempCount].GetComponentInChildren<ListElement>();

        if (_value != 0)
        {
            tempImageMatrix.isAnchor = true;
            tempImageMatrix.DisableButton();
            GameObject go = Instantiate(fishPrefabs[_value-1]);
            go.transform.SetParent(tempImageMatrix.gameObject.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(35, 10); // right - top
            go.GetComponent<RectTransform>().offsetMax = new Vector2(-35, 0); // left - bottom
        }

        cols++;
        if (cols == matrixSize)
        {
            rows++;
            cols = 0;
        }
    }

    private int fishId;
    private Animator selectedAnimator;
    public void FishButton(int _fishId)
    {
        if(selectedAnimator != null)
            selectedAnimator.Play("Normal");

        selectedAnimator = EventSystem.current.currentSelectedGameObject.GetComponent<Animator>();
        selectedAnimator.Play("Pressed");

        fishId = _fishId;
    }

    public void InsertItemToSomePlace()
    {
        Transform _thisSlot = EventSystem.current.currentSelectedGameObject.transform;

        if (_thisSlot.GetComponent<ListElement>().isAnchor == false)
        {
            if (_thisSlot.childCount > 0)
                Destroy(_thisSlot.GetChild(0).gameObject);

            GameObject go = Instantiate(fishPrefabs[fishId - 1]);
            go.transform.SetParent(_thisSlot);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(35, 10); // right - top
            go.GetComponent<RectTransform>().offsetMax = new Vector2(-35, 0); // left - bottom

            int i, j, tempPosInArr;
            tempPosInArr = int.Parse(_thisSlot.name);
            i = tempPosInArr / matrixSize;
            j = tempPosInArr % matrixSize;

            matrix[i, j] = fishId;

            GameObject.FindObjectOfType<HintController>().DisableHintEffects();
        }

        if (MatrixIsFull())
            CheckSudoku();

        ShowMatrix(matrix);
    }

   /* public void DropSomeFishElement(Transform _thisSlot)
    {
        if (_thisSlot.GetComponent<ListElement>().isAnchor == false)
        {
            if (_thisSlot.childCount > 0)
                Destroy(_thisSlot.GetChild(0).gameObject);

            GameObject go = Instantiate(fishPrefabs[DragAndDrop.id - 1]);
            go.transform.SetParent(_thisSlot);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().offsetMin = new Vector2(35, 10); // right - top
            go.GetComponent<RectTransform>().offsetMax = new Vector2(-35, 0); // left - bottom

            int i, j, tempPosInArr;
            tempPosInArr = int.Parse(_thisSlot.name);
            i = tempPosInArr / matrixSize;
            j = tempPosInArr % matrixSize;

            matrix[i, j] = fishId;

            GameObject.FindObjectOfType<HintController>().DisableHintEffects();
        }

        if (MatrixIsFull())
            CheckSudoku();

        ShowMatrix(matrix);
    } */

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

    public GameObject winWindow;
    public GameObject playArea, leftSideBar, rightSideBar;
    public GameObject looseWindow;
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
                        LoseGame();
                    }
                    else
                    {
                        attemptsCount--;
                        if (attemptsCount != 0)
                            StartCoroutine(ShowAttemptsWindow());
                        Debug.Log("Attempts: " + attemptsCount);
                    }
                    return;
                }
            }
        }

        WinGame();
    }

    public void LoseGame()
    {
        Debug.Log("You Lose One Life :(");
        looseWindow.SetActive(true);
        playArea.SetActive(false);
        leftSideBar.SetActive(false);
        rightSideBar.SetActive(false);
    }

    public void WinGame()
    {
        Debug.Log("You Win!!!");
        winWindow.SetActive(true);
        playArea.SetActive(false);
        leftSideBar.SetActive(false);
        rightSideBar.SetActive(false);
    }
}
