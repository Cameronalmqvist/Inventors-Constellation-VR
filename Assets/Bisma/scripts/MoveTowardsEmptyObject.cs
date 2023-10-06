using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveTowardsEmptyObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] public float speed = 5f;

    private bool shouldMove = false;

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
    }

    void Update()
    {
        if (shouldMove)
        {
            MoveTowardsTarget();
        }
    }

    private void OnHoverEnter(XRBaseInteractor interactor)
    {
        shouldMove = true;
    }

    private void OnHoverExit(XRBaseInteractor interactor)
    {
        shouldMove = false;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.up = target.transform.position - transform.position;
    }
}
