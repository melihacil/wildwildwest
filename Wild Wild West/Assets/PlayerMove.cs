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

    public float rotationSpeed = 30f;
    public Vector2 movement;
    public bool jump;
    public bool turnLeft = false;
    public bool turnRight = false;
    // Start is called before the first frame update

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }


    private void rotateLeft()
    {
        turnLeft = false;
        transform.forward = transform.forward * -1;
    }
    private void rotateRight()
    {
        turnRight = false;
        transform.forward = transform.forward * -1;
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

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Player fired");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100f, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 100f);
            if (hit)
            {
                Debug.Log("Player hit " + hit.collider.gameObject.name);
            }
        }
        //if (Input.)

            //Raycast2dhit stores data
            //Physics2d.Raycast 
    }


    private void MovementFunction()
    {
        CalculateSpeed();
        if (movement.x > 0)
        {
            turnLeft = true;
            if (turnRight)
                rotateRight();
            if (moveDirection == -1)
                currentSpeed = 0;
            moveDirection = 1;
        }
        else if (movement.x < 0)
        {
            turnRight = true;
            if (turnLeft)
                rotateLeft();
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
