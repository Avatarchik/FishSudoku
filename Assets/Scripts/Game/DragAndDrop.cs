using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public static int id;
    private GameObject go;
    private GameController go1;

    void Start()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        id = int.Parse(gameObject.name);
    }

    RaycastHit2D hit;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }
    }
}

