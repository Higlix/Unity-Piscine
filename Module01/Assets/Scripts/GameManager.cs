using System;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum PlayerID
{
    PlayerNone = -1,
    Player1 = 0,
    Player2 = 1,
    Player3 = 2,
}

public class Player
{
    public GameObject player;
    public KeyCode KeyID;
    public PlayerID ID;
    public Transform transform;
    public PlayerMovement controller;
}

public class GameManager : MonoBehaviour
{
    [Header("Player GameObjects")]
    public GameObject p1GameObject;
    public GameObject p2GameObject;
    public GameObject p3GameObject;

    [Header("Camera Position")]
    public CameraMove cam;

    private Player[] players = new Player[3];

    private PlayerID currentPlayer = PlayerID.PlayerNone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("START()");
        if (p1GameObject)
        {
            players[0].player = p1GameObject;
            players[0].KeyID = KeyCode.Alpha1;
            players[0].ID = PlayerID.Player1;
            players[0].transform = p1GameObject.GetComponent<Transform>();
            players[0].controller = p1GameObject.GetComponent<PlayerMovement>();
        }
        if (p2GameObject)
        {
            players[1].player = p2GameObject;
            players[1].KeyID = KeyCode.Alpha2;
            players[1].ID = PlayerID.Player2;
            players[1].transform = p2GameObject.GetComponent<Transform>();
            players[1].controller = p2GameObject.GetComponent<PlayerMovement>();
        }
        if (p3GameObject)
        {
            players[2].player = p3GameObject;
            players[2].KeyID = KeyCode.Alpha3;
            players[2].ID = PlayerID.Player3;
            players[2].transform = p3GameObject.GetComponent<Transform>();
            players[2].controller = p3GameObject.GetComponent<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Debug.Log("ALKSDLAKJSD");
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(players[(int)PlayerID.Player1].KeyID))
        {
            currentPlayer = PlayerID.Player1;
            players[(int)PlayerID.Player1].controller.setMove(true);
            players[(int)PlayerID.Player2].controller.setMove(false);
            players[(int)PlayerID.Player3].controller.setMove(false);
        }
        if (Input.GetKeyDown(players[(int)PlayerID.Player2].KeyID))
        {
            currentPlayer = PlayerID.Player2;
            players[(int)PlayerID.Player1].controller.setMove(false);
            players[(int)PlayerID.Player2].controller.setMove(true);
            players[(int)PlayerID.Player3].controller.setMove(false);
        }
        if (Input.GetKeyDown(players[(int)PlayerID.Player3].KeyID))
        {
            currentPlayer = PlayerID.Player3;
            players[(int)PlayerID.Player1].controller.setMove(false);
            players[(int)PlayerID.Player2].controller.setMove(false);
            players[(int)PlayerID.Player3].controller.setMove(true);
        }
        if (currentPlayer != PlayerID.PlayerNone)
        {
            cam.setCameraToPlayer(
                players[(int)currentPlayer].transform.position.x,
                players[(int)currentPlayer].transform.position.y
            );
            
        }
        //transform.position = new Vector3(players[(int)currentPlayer].position.x, players[(int)currentPlayer].position.y + yOffSet, -19f);
    }
}
