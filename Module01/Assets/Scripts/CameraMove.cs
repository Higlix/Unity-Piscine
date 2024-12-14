using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    private KeyCode player1;
    private KeyCode player2;
    private KeyCode player3;

    [Header("Player Transforms")]
    public Transform player1Transform;
    public Transform player2Transform;
    public Transform player3Transform;

    public GameObject player1GO;

    [Header("Camera Movement Speed")]
    public float movementSpeed;

    [Header("Camera Y OffSet")]
    public float yOffSet = 2f;

    private enum playerID
    {
        PlayerNone = -1,
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
    }

    private Transform[] players = new Transform[3];

    private playerID currentPlayer = playerID.PlayerNone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1 = KeyCode.Alpha1;
        player2 = KeyCode.Alpha2;
        player3 = KeyCode.Alpha3;
        if (player1Transform)
            players[0] = player1Transform;
        if (player2Transform)
            players[1] = player2Transform;
        if (player3Transform)
            players[2] = player3Transform;
        transform.position = new Vector3(-16f, 7.5f, -27f);
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
            currentPlayer = playerID.Player1;
        }
        if (Input.GetKeyDown(player2))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player2Transform.position.x, transform.position.y, transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            currentPlayer = playerID.Player2;
        }
        if (Input.GetKeyDown(player3))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player3Transform.position.x, transform.position.y, transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            currentPlayer = playerID.Player3;
        }
        if (currentPlayer != playerID.PlayerNone)
        {
            transform.position = new Vector3(players[(int)currentPlayer].position.x, players[(int)currentPlayer].position.y + yOffSet, transform.position.z);
        }
        
    }
}
