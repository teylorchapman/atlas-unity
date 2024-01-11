using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    public float force = 0.5f;
    public float throwForce = 1.0f;
    private Vector3 startingPosition;
    private bool isThrown;
    private bool onAlley;

    private bool isDragging = false;
    private Plane dragPlane;
    private Vector3 dragOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        startingPosition = transform.position;
        dragPlane = new Plane(Vector3.up, startingPosition);
    }

    private void Update()
    {
        if (onAlley)
        {
            HandleMovementInput();
        }

        if (!isThrown)
        {
            HandleThrowInput();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Interactable"))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartDragging();
                }
            }    
        }

        if (isDragging)
        {
            DragBall();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                StopDragging();
                PrepareThrow();
                ThrowBall();
            }
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * force);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * force);
        }
    }

    private void HandleThrowInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * throwForce);

        if (Input.GetMouseButtonDown(0))
        {
            if (isDragging)
            {
                StopDragging();
                PrepareThrow();
                ThrowBall();
            }

        }
    }

    private void StartDragging()
    {
        isDragging = true;
        rb.isKinematic = true;
        col.enabled = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        dragPlane.Raycast(ray, out distance);
        dragOffset = transform.position - ray.GetPoint(distance);
    }

    private void DragBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (dragPlane.Raycast(ray, out distance))
        {
            Vector3 newPosition = ray.GetPoint(distance) + dragOffset;
            rb.MovePosition(newPosition);
        }
    }

    private void StopDragging()
    {
        isDragging = false;
        rb.isKinematic = false;
        col.enabled = true;
    }

    private void PrepareThrow()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = Camera.main.transform.position + Camera.main.transform.forward *2f;
    }

    private void ThrowBall()
    {
        rb.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);

        isThrown = true;
    }

    private void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startingPosition;
        isThrown = false;
    }

    private IEnumerator Reset(float delay)
    {
        if (onAlley)
        {
            onAlley = false;
            yield return new WaitForSeconds(delay);
            ResetBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Reset")
        {
            ResetBall();
        }
    }
}
