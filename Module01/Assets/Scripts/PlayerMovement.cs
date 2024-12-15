using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float jumpHeight;
    private float movementSpeed;
    private float maxSpeed;
    private float dragForce = 20f;
    private bool pushed = false;
    public int ID = -1;
    public bool canMove = false;
    Rigidbody rb;
    public void Start()
    {
        Debug.Log("Start()");
        jumpHeight = 6f;
        movementSpeed = 25f;
        maxSpeed = 25f;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)    
        {
            HandleMovement(rb);

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
        transform.position = new Vector3(transform.position.x, transform.position.y, -19f);
    }

    private void givePush(PlayerMovement otherCharacter)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
            otherCharacter.rb.AddForce(transform.right * horizontalInput * movementSpeed, ForceMode.Acceleration);

    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerMovement otherCharacter = collision.gameObject.GetComponent<PlayerMovement>();
        if (otherCharacter != null)
        {
            if (canMove)
                otherCharacter.pushed = true;
            if (otherCharacter.pushed && !canMove)
            {
                pushed = true;
                givePush(otherCharacter);
            }
        }
    }

    public void setID(int id)
    {
        ID = id;
    }

    public void setJumpHeight(float newHeight)
    {
        jumpHeight = Mathf.Clamp(newHeight, 2f, 150f);
    }

    public void setMove(bool move)
    {
        canMove = move;
    }
}
