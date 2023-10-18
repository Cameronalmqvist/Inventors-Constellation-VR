using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FloatingOrb : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private Transform target;
    public float normalSpeed = 5f;
    public float maxGazedSpeed = 10.0f;
    public float floatSpeed = 1.0f;
    public float minDistanceFromTarget = 0.5f;

    public GameObject canvasPrefab; // Reference to the canvas prefab

    private Vector3 floatingDirection;
    private Rigidbody rb;
    private bool isGazedUpon = false;
    private GameObject canvasInstance; // Instance of the canvas

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PickRandomDirection();

        // Set up gaze interaction
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(OnGazeEnter);
        interactable.onHoverExited.AddListener(OnGazeExit);

        // Instantiate the canvas from the prefab
        canvasInstance = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
        canvasInstance.SetActive(false);
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (isGazedUpon && distanceToTarget > minDistanceFromTarget)
        {
            // Calculate the direction from the orb to the player's hand.
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Adjust the gazed speed based on distance
            float adjustedGazedSpeed = Mathf.Lerp(normalSpeed, maxGazedSpeed, distanceToTarget / 10f); // Assuming max distance for max speed is 10 units. Adjust as necessary.

            rb.velocity = directionToTarget * adjustedGazedSpeed;
        }
        else
        {
            rb.velocity = floatingDirection * floatSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isGazedUpon) // Only pick a new direction if not being gazed upon
        {
            PickRandomDirection();
        }
    }

    void PickRandomDirection()
    {
        floatingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void OnGazeEnter(XRBaseInteractor interactor)
    {
        Debug.Log("Gaze Entered");
        isGazedUpon = true;
        canvasInstance.SetActive(false);
    }

    void OnGazeExit(XRBaseInteractor interactor)
    {
        Debug.Log("Gaze Exited");
        isGazedUpon = false;

        // Start a coroutine to activate the canvas instance after 5 seconds
        StartCoroutine(ActivateCanvasDelayed(12.0f));
    }

    IEnumerator ActivateCanvasDelayed(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Activate the canvas instance after the delay
        canvasInstance.SetActive(true);

        // Start a coroutine to destroy the canvas instance after 4 seconds
        StartCoroutine(DestroyCanvasDelayed(4.0f));
    }

    IEnumerator DestroyCanvasDelayed(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Destroy the canvas instance after the delay
        Destroy(canvasInstance);
    }
}
