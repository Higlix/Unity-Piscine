using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Player Jump Height")]
    public float jumpHeight;

    [Header("Player Movement Speed")]
    public float movementSpeed;

    [Header("Player Max Speed")]
    public float maxSpeed;

    [Header("Player Drag Force")]
    public float dragForce;

    [Header("Name and ID")]
    public string Name;
    public PlayerID playerID;


    bool isGrounded = false;
    bool canMove = false;
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void LimitVelocity(Rigidbody rigb)
    {
        Vector3 horizontalVelocity = new Vector3(rigb.linearVelocity.x, 0, 0);

        if (Mathf.Abs(horizontalVelocity.x) > maxSpeed)
        {
            float limitedVelocity = Mathf.Sign(horizontalVelocity.x) * maxSpeed;
            rigb.linearVelocity = new Vector3(limitedVelocity, rigb.linearVelocity.y, rigb.linearVelocity.z);
        }
    }

    private void ApplyDrag(Rigidbody rigb)
    {
        Vector3 velocityDrag = -new Vector3(rigb.linearVelocity.x, 0, 0) * dragForce;

        rigb.AddForce(velocityDrag, ForceMode.Acceleration);
    }

    private void HandleMovement(Rigidbody rigb)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            rigb.AddForce(transform.right * horizontalInput * movementSpeed, ForceMode.Acceleration);

            LimitVelocity(rigb);
        }
        else
            ApplyDrag(rigb);
        if (Input.GetKey(KeyCode.Space) && isGrounded && canMove)
        {
            rigb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Update()
    {
        Debug.Log(name);
        Debug.Log("Movement Speed: " + movementSpeed);
        Debug.Log("Jump Height: " + jumpHeight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)    
        {
            HandleMovement(rb);

            //float horizontalInput = Input.GetAxisRaw("Horizontal");

            //Vector3 direction = new Vector3(horizontalInput, 0, 0);

            //transform.Translate(direction * movementSpeed * Time.deltaTime);
        
            //if (Input.GetKey(KeyCode.D))
            //{
            //    rb.Move(new Vector3(transform.position.x + (Time.deltaTime * movementSpeed), transform.position.y, -19f), rb.rotation);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    rb.Move(new Vector3(transform.position.x - (Time.deltaTime * movementSpeed), transform.position.y, -19f), rb.rotation);
            //}
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            //    //transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * jumpHeight), -19f);
            //}

        }
        else
            ApplyDrag(rb);
        transform.position = new Vector3(transform.position.x, transform.position.y, -19f);
    }

    private void givePush(PlayerMovement otherCharacter)
    {
        //HandleMovement(otherCharacter.rb);
        Rigidbody otherRb = otherCharacter.rb;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (otherRb != null && horizontalInput != 0)
        {
            otherRb.AddForce(transform.right * horizontalInput * movementSpeed, ForceMode.Acceleration);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerMovement otherCharacter = collision.gameObject.GetComponent<PlayerMovement>();
        if (otherCharacter != null)
        {
            if (!canMove)
            {
                givePush(otherCharacter);
            }
        }
        if (collision.gameObject.layer == 10)
        {
            isGrounded = true; 
        }
    }
    public void setMove(bool move)
    {
        canMove = move;
    }
}
