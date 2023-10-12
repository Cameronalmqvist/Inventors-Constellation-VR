using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveTowardsEmptyObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] public float normalSpeed = 5f;
    [SerializeField] public float gazedSpeed = 10f;
    [SerializeField] public float amplitude = 0.5f;
    [SerializeField] public float floatSpeed = 0.5f;
    [SerializeField] public Vector3 boundaryMin;
    [SerializeField] public Vector3 boundaryMax;
    [SerializeField] public float currentSpeed;


    private bool shouldMove = false;
    private Vector3 initialPosition;
    private Vector3 randomPosition;
    private Rigidbody rb;
    private Vector3 bounceDirection;
    private Vector3 floatDirection;

    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.onHoverEntered.AddListener(OnHoverEnter);
            interactable.onHoverExited.AddListener(OnHoverExit);
        }
        else
        {
            Debug.LogError("No XRBaseInteractable component found on this GameObject.");
        }

        initialPosition = transform.position;
        SetRandomPosition();

        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = shouldMove; // Set kinematic based on whether it should move towards target initially
        gameObject.AddComponent<SphereCollider>();

        floatDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        currentSpeed = normalSpeed; // Initialize currentSpeed
    }

    void Update()
    {
        if (shouldMove)
        {
            rb.isKinematic = true;
            MoveTowardsTarget();
        }
        else if (Vector3.Distance(transform.position, randomPosition) > 0.1f)
        {
            rb.isKinematic = true;
            RepositionOrb();
        }
        else
        {
            rb.isKinematic = false;
            FloatIdle();
        }

        BoundaryCheck();
    }

    private void OnHoverEnter(XRBaseInteractor interactor)
    {
        shouldMove = true;
        currentSpeed = gazedSpeed;
    }

    private void OnHoverExit(XRBaseInteractor interactor)
    {
        shouldMove = false;
        SetRandomPosition();
        currentSpeed = normalSpeed;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, currentSpeed * Time.deltaTime);
    }

    private void SetRandomPosition()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);
        float randomZ = Random.Range(5f, 10f);
        randomPosition = new Vector3(randomX, randomY, randomZ);
    }

    private void FloatIdle()
    {
        // Use the unique float direction for each orb
        bounceDirection = floatDirection;

        // Modify the floating effect with sinusoidal motion for variation over time
        bounceDirection.x *= Mathf.Sin(Time.time * floatSpeed) * amplitude;
        bounceDirection.y *= Mathf.Sin(Time.time * floatSpeed + Mathf.PI / 4) * amplitude;
        bounceDirection.z *= Mathf.Sin(Time.time * floatSpeed + Mathf.PI / 2) * amplitude;

        rb.velocity = bounceDirection * floatSpeed;
    }

    private void RepositionOrb()
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, randomPosition, step);
    }


    private void BoundaryCheck()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x < boundaryMin.x || currentPosition.x > boundaryMax.x ||
            currentPosition.y < boundaryMin.y || currentPosition.y > boundaryMax.y ||
            currentPosition.z < boundaryMin.z || currentPosition.z > boundaryMax.z)
        {
            bounceDirection = -bounceDirection;
            rb.velocity = bounceDirection * currentSpeed;  // Use current speed here
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
            bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.velocity = bounceDirection * currentSpeed;  // Use current speed here
        }
    }
}
