using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float jumpHeight;

    Rigidbody rb;
    public void Start()
    {
        Debug.Log("Start()");
        jumpHeight = 2f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + (Time.deltaTime * 2f), transform.position.y, -19f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * 2f), transform.position.y, -19f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * jumpHeight), -19f);
        }
        Debug.Log(jumpHeight);
    }

    public void setJumpHeight(float newHeight)
    {
        jumpHeight = Mathf.Clamp(newHeight, 2f, 150f);
    }
}
