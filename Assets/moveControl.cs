using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveControl : MonoBehaviour
{
    public float movementDistance = 5f;
    public float movementSpeed = 2f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isMoving = false;
    private bool isGrounded = true;

    private Vector3 startPosition;
    private Rigidbody2D rb;

    private Button moveButton;
    private Button jumpButton;
    private Button moveBackButton;

    public float delay = 3f;
    public int numberOfCalls = 4;
    private int callsMade = 0;
    private bool isCalling = false;

    private System.Action[] functionCalls;

    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

        functionCalls = new System.Action[] { MoveObjectBackward, Jump, MoveObject, Jump };

        Button startSequenceButton = GameObject.Find("StartSequenceButton").GetComponent<Button>();
        startSequenceButton.onClick.AddListener(StartFunctionCalls);
    }

    public void MoveObject()
    {
        if (!isMoving)
        {
            startPosition = transform.position;
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        isMoving = true;

        float elapsedTime = 0f;
        float currentDistance = 0f;

        while (currentDistance < movementDistance)
        {
            elapsedTime += Time.deltaTime;
            float distanceToMove = Mathf.Lerp(0, movementDistance, elapsedTime * movementSpeed);
            transform.position = startPosition + transform.right * distanceToMove;
            currentDistance = Vector3.Distance(startPosition, transform.position);
            yield return null;
        }

        isMoving = false;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    public void MoveObjectBackward()
    {
        if (!isMoving)
        {
            startPosition = transform.position;
            StartCoroutine(MoveBackward());
        }
    }

    private IEnumerator MoveBackward()
    {
        isMoving = true;

        float elapsedTime = 0f;
        float currentDistance = 0f;

        while (currentDistance < movementDistance)
        {
            elapsedTime += Time.deltaTime;
            float distanceToMove = Mathf.Lerp(0, movementDistance, elapsedTime * movementSpeed);
            transform.position = startPosition - transform.right * distanceToMove;
            currentDistance = Vector3.Distance(startPosition, transform.position);
            yield return null;
        }

        isMoving = false;
    }

    private void StartFunctionCalls()
    {
        if (!isCalling)
        {
            callsMade = 0;
            isCalling = true;
            StartCoroutine(CallFunctions());
        }
    }

    private System.Collections.IEnumerator CallFunctions()
    {
        while (callsMade < numberOfCalls)
        {
            yield return new WaitForSeconds(delay);
            functionCalls[callsMade]();
            callsMade++;
        }

        isCalling = false;
    }
}
