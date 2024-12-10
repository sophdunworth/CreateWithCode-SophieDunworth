using UnityEngine;

public class PresentCollisionHandler : MonoBehaviour
{
    public int scoreIncreaseAmount = 5; // Score when a present is delivered to a child

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log($"Trigger detected with: {other.gameObject.name}, Tag: {other.gameObject.tag}");

        if (other.CompareTag("Child"))  // Check if collided with the "Child"
        {
            Debug.Log("Present collided with a Child!");
            GameManager.Instance.IncreaseScore(scoreIncreaseAmount); // Increase score
            Destroy(gameObject); // Destroy the present
        }
        else if (other.CompareTag("Ground"))  // Check if collided with the "Ground"
        {
            Debug.Log("Present collided with the Ground!");
            Destroy(gameObject); // Destroy the present
        }
    }
}







