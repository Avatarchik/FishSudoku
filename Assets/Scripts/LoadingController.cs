using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour
{
    public static string nextSceneName = null;
    void Start()
    {
        StartCoroutine(GlobalVariables.LoadingSomeScene(nextSceneName, false));
    }
}
