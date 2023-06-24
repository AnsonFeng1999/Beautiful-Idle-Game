using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float panSpeed; // The speed at which the camera pans
    public float panBorder;  // The border size at the edges of the screen for panning
    public Bounds cameraBounds;   // The bounds of the tilemap

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
        else if (mousePosition.x >  Screen.width - panBorder)
        {
            mouseDirection.x = 1;
        }

        if (mousePosition.y < panBorder)
        {
            mouseDirection.y = -1;
        }
        else if (mousePosition.y > Screen.height - panBorder)
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
        float clampedX = Mathf.Clamp(newPosition.x, cameraBounds.min.x, cameraBounds.max.x);
        float clampedY = Mathf.Clamp(newPosition.y, cameraBounds.min.y, cameraBounds.max.y);
        newPosition = new Vector3(clampedX, clampedY, newPosition.z);

        // Apply the pan amount to the camera's position
        transform.position = newPosition;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
    }
    
    
}
