using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LerpFloatingOrb : MonoBehaviour
{
    [Header("Lerp Parameters")]
    public Transform playerTransform;
    public float lerpSpeed = 1f;
    public Vector3 offsetFromPlayer = new Vector3(0, 0, 2f);
    public UnityEngine.Events.UnityEvent onOrbTouched;
    private XRSimpleInteractable interactable;

    [Header("Prefab Spawn Parameters")]
    public GameObject prefabToSpawn; // Drag the prefab you want to spawn in the inspector
    public UnityEvent onLerpComplete; // Event triggered after orb is in position for 2 seconds

    private bool shouldLerp = false;
    private Vector3 targetPosition;
    private bool isAtTarget = false;
    private float timeAtTarget = 0f;
    private GameObject spawnedPrefab;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.onSelectEntered.AddListener(HandleOrbTouched);
    }

    void Update()
    {
        if (shouldLerp)
        {
            targetPosition = playerTransform.position + playerTransform.forward * offsetFromPlayer.z +
                             playerTransform.up * offsetFromPlayer.y;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f) // Approximate check
            {
                isAtTarget = true;
            }
        }

        if (isAtTarget)
        {
            timeAtTarget += Time.deltaTime;
            if (timeAtTarget >= 2f)
            {
                onLerpComplete.Invoke();
                isAtTarget = false; // To ensure event is not called repeatedly
            }
        }
    }

    public void StartLerping()
    {
        shouldLerp = true;
    }

    public void SpawnPrefab()
    {
        spawnedPrefab = Instantiate(prefabToSpawn, transform.position + Vector3.up, Quaternion.identity);
        Destroy(spawnedPrefab, 20f);
    }

    private void HandleOrbTouched(XRBaseInteractor interactor)
    {
        onOrbTouched?.Invoke();
    }
}
