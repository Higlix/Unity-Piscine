using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    [Header("Player GameObjects")]
    public GameObject player1GO;
    public GameObject player2GO;
    public GameObject player3GO;

    [Header("Camera Y OffSet")]
    public float yOffSet = 2f;

    private KeyCode player1;
    private KeyCode player2;
    private KeyCode player3;

    private enum playerID
    {
        PlayerNone = -1,
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
    }

    private PlayerMovement PlayerController1;
    private PlayerMovement PlayerController2;
    private PlayerMovement PlayerController3;
    private Transform[] players = new Transform[3];

    private playerID currentPlayer = playerID.PlayerNone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1 = KeyCode.Alpha1;
        player2 = KeyCode.Alpha2;
        player3 = KeyCode.Alpha3;
        if (player1GO)
        {
            players[0] = player1GO.GetComponent<Transform>();
            PlayerController1 = player1GO.GetComponent<PlayerMovement>();
        }
        if (player2GO)
        {
            players[1] = player2GO.GetComponent<Transform>();
            PlayerController2 = player2GO.GetComponent<PlayerMovement>();
        }
        if (player3GO)
        {
            players[2] = player3GO.GetComponent<Transform>();
            PlayerController3 = player3GO.GetComponent<PlayerMovement>();
        }
        PlayerController2.Start();
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
            PlayerController1.enabled = true;
            PlayerController2.enabled = false;
            PlayerController3.enabled = false;
            PlayerController1.setJumpHeight(2f);
        }
        if (Input.GetKeyDown(player2))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player2Transform.position.x, transform.position.y, transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            currentPlayer = playerID.Player2;
            PlayerController1.enabled = false;
            PlayerController2.enabled = true;
            PlayerController3.enabled = false;
            PlayerController2.setJumpHeight(5f);
        }
        if (Input.GetKeyDown(player3))
        {
            //transform.position = Vector3.Lerp(
            //    transform.position,
            //    new Vector3(player3Transform.position.x, transform.position.y, transform.position.z),
            //    Time.deltaTime * movementSpeed
            //);
            currentPlayer = playerID.Player3;
            PlayerController1.enabled = false;
            PlayerController2.enabled = false;
            PlayerController3.enabled = true;
            PlayerController3.setJumpHeight(3f);
        }
        if (currentPlayer != playerID.PlayerNone)
        {
            transform.position = new Vector3(players[(int)currentPlayer].position.x, players[(int)currentPlayer].position.y + yOffSet, transform.position.z);
        }
        //transform.position = new Vector3(players[(int)currentPlayer].position.x, players[(int)currentPlayer].position.y + yOffSet, -19f);
    }
}
