using UnityEngine;

public class DragAndPlace : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 initialPosition;

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            initialPosition = transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            // Perform any additional logic or actions upon releasing the object
        }
    }
}
