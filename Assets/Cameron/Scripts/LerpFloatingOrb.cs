using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LerpFloatingOrb : MonoBehaviour
{
    [Header("Random Floating Parameters")]
    public float floatSpeed = 0.5f;
    public float changeDirectionInterval = 2.0f;

    private Vector3 currentRandomDirection;
    private float timeInCurrentDirection = 0f;

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
    private Vector3 nextRandomDirection;
    Vector3 previousRandomDirection;

    private void Start()
    {
        PickNewRandomDirection();
    }

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
        else
        {
            FloatRandomly();
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

    void FloatRandomly()
    {
        transform.position += currentRandomDirection * floatSpeed * Time.deltaTime;
        timeInCurrentDirection += Time.deltaTime;

        if (timeInCurrentDirection > changeDirectionInterval)
        {
            previousRandomDirection = currentRandomDirection;
            PickNewRandomDirection();
            timeInCurrentDirection = 0f;
        }
        else
        {
            // Slerp between the current direction and the next one for smoother transitions
            currentRandomDirection = Vector3.Slerp(previousRandomDirection, nextRandomDirection, timeInCurrentDirection / changeDirectionInterval);
        }
    }


    void PickNewRandomDirection()
    {
        nextRandomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
