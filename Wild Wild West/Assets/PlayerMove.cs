using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float horizontalInput, verticalInput;
    [SerializeField]
    private float maxVelocity = 5f;
    public float currentSpeed = 0f;
    private float acceleration = 10f;
    public float jumpForce = 1f;
    public float moveDirection = 1;
    private Rigidbody2D playerRigidBody;


    public Vector2 movement;
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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space))
            jump=  true;
        else 
            jump= false;
        MovementFunction();
    }


    private void MovementFunction()
    {
        CalculateSpeed();
        if (movement.x > 0)
        {
            if (moveDirection == -1)
                currentSpeed = 0;
            moveDirection = 1;
        }
        else if (movement.x < 0)
        {
            if (moveDirection == 1)
                currentSpeed = 0;
            moveDirection= -1;
        }

    }

    private void CalculateSpeed()
    {
        if (Mathf.Abs(movement.x) > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            //Accelerate => deaccelerate 
            currentSpeed -= acceleration * Time.deltaTime;
        }    
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxVelocity);
        //ClampSpeed();
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(currentSpeed * moveDirection, playerRigidBody.velocity.y);
        //playerRigidBody.velocity = (Vector2)transform.forward * currentSpeed * moveDirection * Time.fixedDeltaTime;
        //playerRigidBody.velocity = currentSpeed * moveDirection * Time.fixedDeltaTime;
        if (jump)
            playerRigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
