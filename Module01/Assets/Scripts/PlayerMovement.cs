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

    int GroundLayer = 10;
    int WallLayer = 11;

    bool isGrounded = false;
    bool canMove = false;
    Rigidbody rb;

    bool canMoveLeft = true;
    bool canMoveRight = true;

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

        rigb.AddForce(velocityDrag, ForceMode.Force);
    }

    private void HandleMovement(Rigidbody rigb)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0 && canMoveLeft)
        {
            rigb.AddForce(transform.right * horizontalInput * movementSpeed, ForceMode.Force);

            LimitVelocity(rigb);
        }
        else if (horizontalInput > 0 && canMoveRight)
        {
            rigb.AddForce(transform.right * horizontalInput * movementSpeed, ForceMode.Force);

            LimitVelocity(rigb);
        }
        //else
            //ApplyDrag(rigb);
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rigb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement(rb);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == GroundLayer)
        {
            isGrounded = false;
        }
        if (collision.gameObject.layer == WallLayer)
        {
            canMoveRight = true;
            canMoveLeft = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == GroundLayer)
        {
            isGrounded = true; 
        }
        if (collision.gameObject.layer == WallLayer)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Debug.Log(horizontalInput);
            if (horizontalInput < 0)
            {
                canMoveLeft = false;
                canMoveRight = true;
            }
            else if (horizontalInput > 0)
            {
                canMoveRight = false;
                canMoveLeft = true;
            }
        }
    }
    public void setMove(bool move)
    {
        canMove = move;
    }
}
