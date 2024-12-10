using UnityEngine;
 

//Initial code to control the child animations
//Unable to work just to character selection
public class ChildAnimatorController : MonoBehaviour
{
    private Animator animator;
 
    private static class AnimationTriggers
    {
        public const string Jump = "Jump";
        public const string Victory = "Victory";
        public const string Wave = "Wave";
    }
 
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError($"Animator component is missing on {gameObject.name}. Please ensure the prefab includes an Animator component.");
            enabled = false; 
        }
    }
 
    // Start the jump animation before present is recieved
    public void StartJumpAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(AnimationTriggers.Jump);
        }
    }
 
    // Trigger the recieved present animation when a present collides with the child object
    public void ReceivePresent()
    {
        if (animator != null)
        {
            animator.SetTrigger(AnimationTriggers.Victory);
        }
    }
 
    // Trigger the wave animation 
    public void MissPresent()
    {
        if (animator != null)
        {
            animator.SetTrigger(AnimationTriggers.Wave);
        }
    }
 
    // Get the duration of the jump animation
    public float GetJumpAnimationDuration()
    {
        if (animator != null)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "Jump")
                {
                    return clip.length;
                }
            }
            Debug.LogError("Jump animation clip not found on " + gameObject.name);
        }
        return 0f;
    }
}
