using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    private bool isDragging = false;
    private Grid grid;
    private Vector3 offset;

    private void Start()
    {
        grid = FindObjectOfType<Grid>(); // Assumes you have a Grid component in the scene
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
        SnapToGrid();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;
            transform.position = targetPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void SnapToGrid()
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        Vector3 snappedPosition = grid.CellToWorld(cellPosition);
        snappedPosition.x += grid.cellSize.x * 0.5f;
        snappedPosition.y += grid.cellSize.y * 0.5f;
        transform.position = snappedPosition;
    }
}
