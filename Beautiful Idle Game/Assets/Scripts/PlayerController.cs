using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;                // Speed at which the player moves
    private Rigidbody2D playerRb2d;                 // Player Rigidbody
    private SpriteRenderer playerSpriteRenderer;    // Player SpriteRenderer for changing orientation when moving


    private void Start()
    {
        playerRb2d= GetComponent<Rigidbody2D>();
        playerSpriteRenderer= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(horizontalInput, verticalInput);
        input = Vector2.ClampMagnitude(input, 1);
        Vector2 movement = input * movementSpeed;
        playerRb2d.position += movement * Time.deltaTime;
    }
}