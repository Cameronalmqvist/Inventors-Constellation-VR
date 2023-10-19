using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ResetHandler : MonoBehaviour
{
    public ScreenFader screenFader; 
    public ActionBasedController controller;
    private bool wasPressedLastFrame;

    void Start()
    {
        screenFader = FindObjectOfType<ScreenFader>();
    }

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
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        
        screenFader.FadeOut();
        yield return new WaitForSeconds(1);

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
        screenFader.FadeIn();
    }

}
