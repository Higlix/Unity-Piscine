using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    void Start()
    {
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
            transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * 2f), -19f);
        }
    }
}
