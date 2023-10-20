using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform playerCamera;  // Assign the VR camera (usually the main camera in VR setups) here in the inspector.

    void Update()
    {
        // Position the canvas 1.5m in front of the player's camera.
        transform.position = playerCamera.position + playerCamera.forward * 1.5f;

        // Rotate the canvas to face the player.
        transform.LookAt(playerCamera.position);

        // Invert the rotation around the Y-axis so it's facing towards the camera.
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
    }
}
