using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FloatingOrb : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float floatSpeed = 1.0f;
    public float gazedSpeed = 3.0f;

    private Vector3 floatingDirection;
    private Rigidbody rb;
    private bool isGazedUpon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PickRandomDirection();

        // Set up gaze interaction
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(OnGazeEnter);
        interactable.onHoverExited.AddListener(OnGazeExit);
    }

    void Update()
    {
        
        if (isGazedUpon)
        {
            // Move towards player when gazed upon
            Vector3 directionToPlayer = (Camera.main.transform.position - transform.position).normalized;
            rb.velocity = directionToPlayer * gazedSpeed;
        }
        else
        {
            // Regular floating movement
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
        PickRandomDirection();  // After stopping the gaze, the orb should float in a new direction
    }
}