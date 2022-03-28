using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public Vector3 rotationpoint;

    private const int GridSizeX = 10;
    private const int GridSizeY = 19;
    public static Transform[,] Grid = new Transform[GridSizeX, GridSizeY];

    private float previousTime;
    private float fallTime = 1f;

    void Start()
    {
        StartCoroutine(MoveDown());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 1, Space.World);

            if (!ValidMove())
                transform.Translate(Vector3.right * 1, Space.World);
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 1, Space.World);

            if (!ValidMove())
                transform.Translate(Vector3.left * 1, Space.World);
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationpoint), new Vector3(0, 0, 1), 90);

            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationpoint), new Vector3(0, 0, 1), -90);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(MoveFastDown());

        }
    }

    IEnumerator MoveDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            transform.Translate(0, -1, 0, Space.World);

            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);

            }
        }
    }

    IEnumerator MoveFastDown()
    {
        yield return new WaitForSeconds(0.1f);
        transform.Translate(0, -1, 0, Space.World);

        if (!ValidMove())
        {
            transform.position += new Vector3(0, 1, 0);
            AddGrid();
            check();
            this.enabled = false;
            FindObjectOfType<SpawnManager>().spawn();
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedx = Mathf.RoundToInt(children.transform.position.x);
            int roundedy = Mathf.RoundToInt(children.transform.position.y);
            if (roundedx < 0 || roundedx >= GridSizeX || roundedy < 0 || roundedy >= GridSizeY)
            {
                return false;
            }
            if (Grid[roundedx, roundedy] != null)        //Grid boþ deðilse
            {
                return false;
            }
        }
        return true;
    }

    void AddGrid()
    {
        foreach (Transform children in transform)     //çocuklarýn pozisyonlarý gride atanýr.
        {
            int roundedx = Mathf.RoundToInt(children.transform.position.x);
            int roundedy = Mathf.RoundToInt(children.transform.position.y);

            Grid[roundedx, roundedy] = children;
        }
    }
    void check()
    {
        for (int i = GridSizeY - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    bool HasLine(int i)
    {
        for (int j = 0; j < GridSizeX; j++)
        {
            if (Grid[j, i] == null)
                return false;
        }
        return true;
    }
    void DeleteLine(int i)
    {

        for (int j = 0; j < GridSizeX; j++)
        {
            Destroy(Grid[j, i].gameObject);
            Grid[j, i] = null;

        }

    }
    void RowDown(int i)
    {
        for (int y = i; y < GridSizeY; y++)
        {
            for (int j = 0; j < GridSizeX; j++)
                if (Grid[j, y] != null)
                {
                    Grid[j, y - 1] = Grid[j, y];
                    Grid[j, y] = null;
                    Grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);

                }

        }
    }



}






