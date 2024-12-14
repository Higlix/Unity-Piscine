using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private KeyCode player1;
    private KeyCode player2;
    private KeyCode player3;


    [Header("Player Transforms")]
    public Transform player1Transform;
    public Transform player2Transform;
    public Transform player3Transform;

    [Header("Camera Movement Speed")]
    public float movementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1 = KeyCode.Alpha1;
        player2 = KeyCode.Alpha2;
        player3 = KeyCode.Alpha3;
        //transform.position = new Vector3(-16f, 6f, -24f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(player1))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player1Transform.position.x, transform.position.y , transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            transform.position = new Vector3(player1Transform.position.x, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(player2))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player2Transform.position.x, transform.position.y, transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            transform.position = new Vector3(player2Transform.position.x, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(player3))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player3Transform.position.x, transform.position.y, transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            transform.position = new Vector3(player3Transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
