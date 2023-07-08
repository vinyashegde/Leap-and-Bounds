using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class gridRenderer : MonoBehaviour
{
    public int gridSizeX = 10; // Number of cells in the x-axis
    public int gridSizeY = 10; // Number of cells in the y-axis
    public float cellSize = 1f; // Size of each grid cell

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        lineRenderer.positionCount = (gridSizeX + gridSizeY + 2) * 2;

        float width = gridSizeX * cellSize;
        float height = gridSizeY * cellSize;

        // Draw horizontal lines
        for (int y = 0; y <= gridSizeY; y++)
        {
            float yPos = y * cellSize;
            lineRenderer.SetPosition(y * 2, new Vector3(0, yPos, 0));
            lineRenderer.SetPosition(y * 2 + 1, new Vector3(width, yPos, 0));
        }

        // Draw vertical lines
        int indexOffset = (gridSizeY + 1) * 2;
        for (int x = 0; x <= gridSizeX; x++)
        {
            float xPos = x * cellSize;
            lineRenderer.SetPosition(x * 2 + indexOffset, new Vector3(xPos, 0, 0));
            lineRenderer.SetPosition(x * 2 + 1 + indexOffset, new Vector3(xPos, height, 0));
        }
    }
}
