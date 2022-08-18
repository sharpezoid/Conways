using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool On = false;
    MeshRenderer meshRenderer;
    public Vector2Int Pos;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //toggle cell.
            SetCellActive(!On);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<FloodFill>().StartFill(Pos);
        }
    }

    public void SetCellActive(bool on)
    {
        On = on;
        if (On)
            meshRenderer.material.color = Color.black;
        else
            meshRenderer.material.color = Color.white;
    }
}
