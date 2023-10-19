using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ResetHandler : MonoBehaviour
{
    public ActionBasedController controller;
    private bool wasPressedLastFrame;

    void Update()
    {
        bool isPressed = controller.selectAction.action?.ReadValue<float>() > 0.5f; // Using the select action directly

        if (wasPressedLastFrame && !isPressed)
        {
            ResetExperience();
        }

        wasPressedLastFrame = isPressed;
    }

    void ResetExperience()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
