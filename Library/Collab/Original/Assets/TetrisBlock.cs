﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TetrisBlock : MonoBehaviour
{    
    public Vector3 rotationPoint;

    private float previousTime;
    public float fallTime = 0.8f;

    public static int height = 20;
    public static int width = 10;

    public static int maxLine;
    public static int deleteLine;
    public static int linesCleared;

    public static bool lineCleared = false;
    public static bool gameOver = false;

    public static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        deleteLine = 0;
        // Default position not valid? Then it's game over
        if (!ValidMove())
        {
            gameOver = true;
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                
                this.enabled = false;
                FindObjectOfType<Spawner>().NewTetromino();
            }                
            previousTime = Time.time;
        }
    }

    void CheckForLines()
    {
        for (int i = height-1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                deleteLine += 1;
                lineCleared = true;
                DeleteLine(i);
                RowDown(i);
            }
        }
        linesCleared = deleteLine;
        maxLine -= linesCleared;
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int i)
    {                
        for (int j = 0; j < width; j++)
        {            
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {            
            for (int j = 0; j < width; j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);                    
                }
            }            
        }
    }

    void AddToGrid()
    {
        linesCleared = 0;
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;

            if (roundedY > maxLine)
            {
                maxLine = roundedY;
            }
            
        }        
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
}
