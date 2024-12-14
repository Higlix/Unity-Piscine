using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float jumpHeight;
    private float movementSpeed;
    private float maxSpeed;
    private float dragForce = 2f;
    private bool canMove = false;
    Rigidbody rb;
    public void Start()
    {
        Debug.Log("Start()");
        jumpHeight = 6f;
        movementSpeed = 5f;
        maxSpeed = 10f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void LimitVelocity()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, 0);

        if (Mathf.Abs(horizontalVelocity.x) > maxSpeed)
        {
            float limitedVelocity = Mathf.Sign(horizontalVelocity.x) * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity, rb.linearVelocity.y, rb.linearVelocity.z);
        }
    }

    private void ApplyDrag()
    {
        Vector3 velocityDrag = -new Vector3(rb.linearVelocity.x, 0, 0) * dragForce;

        rb.AddForce(velocityDrag, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            
            if (horizontalInput != 0)
            {
                rb.AddForce(Vector3.right * horizontalInput * movementSpeed, ForceMode.Acceleration);

                LimitVelocity();
            }
            else
                ApplyDrag();

            //if (Input.GetKey(KeyCode.D))
            //{
            //    rb.AddForce(transform.right * movementSpeed, ForceMode.Acceleration);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{ 
            //    rb.AddForce(-transform.right * movementSpeed, ForceMode.Acceleration);
            //}
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            //    //transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * jumpHeight), -19f);
            //}
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -19f);
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
