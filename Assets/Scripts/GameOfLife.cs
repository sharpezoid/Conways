using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public int Height, Width;
    private Cell[,] cells;
    private bool[,] cellBuffer;

    bool playing = false;

    float lastTick;
    float tickDuration = 0.5f;

    void Start()
    {
        lastTick = Time.time;
        cells = new Cell[Width, Height];
        cellBuffer = new bool[Width, Height];

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
                Cell newCell = newCube.AddComponent<Cell>();
                cells[x, y] = newCell;
            }
        }
    }

    private void Update()
    {
        if (!playing)
            return;

        if (Time.time > lastTick + tickDuration)
        {
            SimulateTick();
            lastTick = Time.time;
        }
    }

    void SimulateTick()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int aliveNeighbourCount = GetNumberOfNeighbours(x, y);

                if (cellBuffer[x, y])
                {
                    if (aliveNeighbourCount < 2)
                        cells[x, y].SetCellActive(false);

                    else if (aliveNeighbourCount > 3)
                        cells[x, y].SetCellActive(false);

                    else
                        cells[x, y].SetCellActive(true);
                }
                else
                {
                    if (aliveNeighbourCount == 3)
                        cells[x, y].SetCellActive(true);
                }
            }
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                cellBuffer[x, y] = cells[x, y].On;
            }
        }
    }

    int GetNumberOfNeighbours(int _x, int _y)
    {
        int numberAlive = 0;

        for (int y = _y - 1; y <= _y + 1; y++)
        {
            for (int x = _x - 1; x <= _x + 1; x++)
            {
                if (!((x < 0 || y < 0) || (x >= Height || y >= Width))
                    && !( x==_x && y == _y)
                    && cellBuffer[x,y])
                {
                    numberAlive++;
                }
            }
        }

        return numberAlive;
    }

    public void TogglePlay()
    {
        playing = !playing;
    }
}
