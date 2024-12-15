using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    [Header("Camera Y OffSet")]
    public float yOffSet = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(-16f, 7.5f, -27f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCameraToPlayer(float playerX, float playerY)
    {
        transform.position = new Vector3(playerX, playerY + yOffSet, transform.position.z);
    }
}
