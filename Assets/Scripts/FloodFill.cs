using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    public int Height, Width;
    private Cell[,] cells;

    bool playing = false;

    float lastTick;
    float tickDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[Width, Height];
        GenerateMap();

        Camera.main.transform.position = new Vector3(Width / 2f, Height / 2f, -10);
        Camera.main.orthographicSize = Height / 2f;
    }

    void GenerateMap()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = new Vector3(x, y, 0);
                cells[x, y] = newCube.AddComponent<Cell>();
                cells[x, y].Pos = new Vector2Int(x, y);
            }
        }
    }

    public void StartFill(Vector2Int pos)
    {
        StartCoroutine(Fill(pos.x, pos.y));
    }

    IEnumerator Fill(int x, int y)
    {
        if (!((x < 0 || y < 0) || (x >= Height || y >= Width)))
        {
            if (!cells[x, y].On)
            {
                cells[x, y].SetCellActive(true);
                yield return new WaitForSeconds(0.05f);
                StartCoroutine(Fill(x + 1, y));
                StartCoroutine(Fill(x - 1, y));
                StartCoroutine(Fill(x, y + 1));
                StartCoroutine(Fill(x, y - 1));
            }
        }

        yield return new WaitForSeconds(0.05f);
    }

}
