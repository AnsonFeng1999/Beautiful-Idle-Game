using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraPan : MonoBehaviour
{
    public float panSpeed = 10f;   // The speed at which the camera pans
    public float panBorder = 10f;  // The border size at the edges of the screen for panning
    public Tilemap map;
    private float screenWidth;     // The width of the screen in pixels
    private float screenHeight;    // The height of the screen in pixels
    private Bounds tilemapBounds;   // The bounds of the tilemap

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        // Calculate the bounds of the tilemap
        tilemapBounds = map.localBounds;
    }

    private void Update()
    {
        // Calculate the pan direction based on user input or mouse position
        Vector3 panDirection = GetPanDirection();

        // Pan the camera
        PanCamera(panDirection);
    }

    private Vector3 GetPanDirection()
    {
        // Get the direction from user input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 keyboardDirection = new Vector3(horizontalInput, verticalInput, 0f);

        // Get the direction from mouse position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseDirection = Vector3.zero;

        if (mousePosition.x < panBorder)
        {
            mouseDirection.x = -1;
        }
        else if (mousePosition.x > screenWidth - panBorder)
        {
            mouseDirection.x = 1;
        }

        if (mousePosition.y < panBorder)
        {
            mouseDirection.y = -1;
        }
        else if (mousePosition.y > screenHeight - panBorder)
        {
            mouseDirection.y = 1;
        }

        // Return the combined direction
        return keyboardDirection + mouseDirection;
    }

    private void PanCamera(Vector3 direction)
    {
        // Calculate the pan amount based on direction and pan speed
        Vector3 panAmount = direction * panSpeed * Time.deltaTime;

        // Calculate the new position of the camera
        Vector3 newPosition = transform.position + panAmount;

        // Clamp the camera position within the tilemap bounds
        float clampedX = Mathf.Clamp(newPosition.x, tilemapBounds.min.x, tilemapBounds.max.x);
        float clampedY = Mathf.Clamp(newPosition.y, tilemapBounds.min.y, tilemapBounds.max.y);
        newPosition = new Vector3(clampedX, clampedY, newPosition.z);

        // Apply the pan amount to the camera's position
        transform.position = newPosition;
    }
}
