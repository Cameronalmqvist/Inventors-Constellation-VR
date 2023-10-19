using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if you want to stop the animation, e.g., after 12 seconds
        if (Time.time >= 12f)
        {
            // Stop the animation
            animator.enabled = false;
        }
    }
}

