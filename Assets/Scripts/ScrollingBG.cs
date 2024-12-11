using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Speed of vertical scrolling
    private Vector2 startPos;

    private void Start()
    {
        // Store the initial position of the background
        startPos = transform.position;
    }

    private void Update()
    {
        
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 20f);
        transform.position = startPos + Vector2.down * newPos; 
    }
}
