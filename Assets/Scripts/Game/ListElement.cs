using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListElement : MonoBehaviour
{
    //[HideInInspector]
    public bool isAnchor = false;

    public void DisableButton()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
    }
}
