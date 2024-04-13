//using UnityEngine;

//public class UI_FadeScreen : MonoBehaviour
//{
//    private Animator animator;

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    public void FadeOut() => animator.SetTrigger("fadeOut");
//    public void FadeIn() => animator.SetTrigger("fadeIn");
//}

using UnityEngine;

public class UI_FadeScreen : MonoBehaviour
{
    private Animator animator;

    // Add Awake method to ensure it's executed before Start
    private void Awake()
    {
        // Use GetComponentInChildren to find the Animator component
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found in UI_FadeScreen.", this);
        }
    }

    public void FadeOut()
    {
        // Check if animator is not null before calling SetTrigger
        if (animator != null)
        {
            animator.SetTrigger("fadeOut");
        }
        else
        {
            Debug.LogWarning("Animator is null. Unable to fade out.");
        }
    }

    public void FadeIn()
    {
        // Check if animator is not null before calling SetTrigger
        if (animator != null)
        {
            animator.SetTrigger("fadeIn");
        }
        else
        {
            Debug.LogWarning("Animator is null. Unable to fade in.");
        }
    }
}