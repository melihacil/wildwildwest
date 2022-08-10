using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float horizontalInput, verticalInput;
    public float maxVelocity = 3;
    public float currentSpeed = 0;
    public float jumpForce = 1f;
    private Rigidbody2D playerRigidBody;

    public bool jump;
    // Start is called before the first frame update

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal a-d Vertical w-s
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space))
            jump=  true;
        else 
            jump= false;
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        if (horizontalInput > 0)
        {
            currentSpeed += 1;
        }
        else if (horizontalInput < 0)
        {
            currentSpeed -= 1;
        }
        else currentSpeed = 0;
        Mathf.Clamp(currentSpeed, 0, maxVelocity);
    }
    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(currentSpeed, playerRigidBody.velocity.y); 
        if (jump)
            playerRigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
