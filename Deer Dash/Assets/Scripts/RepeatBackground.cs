using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public float scrollSpeed = 5f; // Speed at which the background scrolls
    private float backgroundWidth; // Width of the background segment
    private Transform[] segments; // Array to store the two background segments
    private bool startMoving = false; // Flag to control whether the background should move

    void Start()
    {
        // Ensure that the object has exactly two child objects for the scrolling background
        if (transform.childCount < 2)
        {
            Debug.LogError("RepeatBackground requires exactly two child objects. Check your hierarchy setup.");
            return;
        }

        // Initialize the segments array with the two child objects
        segments = new Transform[2];
        segments[0] = transform.GetChild(0);
        segments[1] = transform.GetChild(1);

        
        if (!segments[0].GetComponent<SpriteRenderer>() || !segments[1].GetComponent<SpriteRenderer>())
        {
            Debug.LogError("Both background segments must have a SpriteRenderer component.");
            return;
        }

        
        backgroundWidth = segments[0].GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("Background width: " + backgroundWidth); 
    }

    // Method to start the background movement
    public void StartBackgroundMovement()
    {
        startMoving = true; 
        Debug.Log("Background movement started!"); 
    }

    // Method to stop the background movement
    public void StopBackgroundMovement()
    {
        startMoving = false; 
        Debug.Log("Background movement stopped!");
    }

    void Update()
    {
        // Only move the background if the startMoving flag is true
        if (startMoving)
        {
           
            foreach (Transform segment in segments)
            {
                segment.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
            }

           
            if (segments[0].position.x <= -backgroundWidth)
            {
                RepositionSegment(0); 
            }

            if (segments[1].position.x <= -backgroundWidth)
            {
                RepositionSegment(1); 
            }
        }
    }

    // Method to reposition a segment to the end of the other segment
    private void RepositionSegment(int index)
    {
        int otherIndex = (index == 0) ? 1 : 0;
        // Position the segment at the end of the other segment
        segments[index].position = new Vector3(segments[otherIndex].position.x + backgroundWidth,
                                               segments[index].position.y,
                                               segments[index].position.z);
    }
}

