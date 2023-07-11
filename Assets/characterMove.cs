using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class characterMove : MonoBehaviour
{
    public Grid grid;
    public float moveSpeed = 5f;
    public Transform characterTransform;

    private Vector3Int currentGridPosition;
    private Vector3Int targetGridPosition;
    private bool isMoving = false;

    private void Awake()
    {
        if (grid == null)
        {
            Debug.LogError("Grid component not assigned. Please assign the Grid component in the inspector.");
        }
        else
        {
            currentGridPosition = grid.WorldToCell(characterTransform.position);
        }
    }

    public void MoveRight()
    {
        MoveToDirection(new Vector3Int(1, 0, 0));
    }

    public void MoveLeft()
    {
        MoveToDirection(new Vector3Int(-1, 0, 0));
    }

    public void MoveUp()
    {
        MoveToDirection(new Vector3Int(0, 1, 0));
    }

    public void MoveDown()
    {
        MoveToDirection(new Vector3Int(0, -1, 0));
    }

    private void MoveToDirection(Vector3Int direction)
    {
        if (!isMoving)
        {
            targetGridPosition = currentGridPosition + direction;
            if (IsWithinGridBounds(targetGridPosition))
            {
                Vector3 targetWorldPosition = grid.CellToWorld(targetGridPosition);
                StartCoroutine(MoveToPosition(targetWorldPosition));
            }
        }
    }

    private bool IsWithinGridBounds(Vector3Int position)
    {
        Tilemap tilemap = grid.GetComponentInChildren<Tilemap>();
        return tilemap.cellBounds.Contains(position);
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3Int targetCellPosition = grid.WorldToCell(targetPosition);
        Vector3 targetCellWorldPosition = grid.CellToWorld(targetCellPosition);

        while (Vector3.Distance(characterTransform.position, targetCellWorldPosition) > 0.05f)
        {
            characterTransform.position = Vector3.MoveTowards(characterTransform.position, targetCellWorldPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        characterTransform.position = targetCellWorldPosition;
        currentGridPosition = targetGridPosition;
        isMoving = false;
    }


}
