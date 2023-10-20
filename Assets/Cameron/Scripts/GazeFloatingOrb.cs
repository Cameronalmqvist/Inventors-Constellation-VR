using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class GazeFloatingOrb : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float normalSpeed = 5f;
    public float maxGazedSpeed = 10.0f;
    public float floatSpeed = 1.0f;
    public UnityEvent onOrbTouched;

    private Vector3 floatingDirection;
    private Rigidbody rb;
    private bool isGazedUpon = false;
    private XRSimpleInteractable interactable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PickRandomDirection();

        // Set up gaze interaction
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.onSelectEntered.AddListener(OnOrbTouched);
        interactable.onHoverEntered.AddListener(OnGazeEnter);
        interactable.onHoverExited.AddListener(OnGazeExit);
    }

    void Update()
    {
        Vector3 directionToPlayer = Vector3.zero; // Initialize with zero
        float distanceToPlayer = 0f; // Initialize with 0

        if (isGazedUpon)
        {
            // Calculate the direction from the orb to the player's hand/controller.
            if (interactable.hoveringInteractors.Count > 0)
            {
                XRBaseInteractor firstInteractor = interactable.hoveringInteractors[0];
                directionToPlayer = (firstInteractor.transform.position - transform.position).normalized;
                distanceToPlayer = Vector3.Distance(transform.position, firstInteractor.transform.position);
            }

            // Adjust the gazed speed based on distance
            float adjustedGazedSpeed = Mathf.Lerp(normalSpeed, maxGazedSpeed, distanceToPlayer / 10f); // Assuming max distance for max speed is 10 units.

            rb.velocity = directionToPlayer * adjustedGazedSpeed;
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
        isGazedUpon = true;
    }

    void OnGazeExit(XRBaseInteractor interactor)
    {
        isGazedUpon = false;
    }

    void OnOrbTouched(XRBaseInteractor interactor)
    {
        onOrbTouched?.Invoke();
    }
}
