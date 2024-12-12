using UnityEngine;
using UnityEngine.Animations;

public class CamMove : MonoBehaviour
{
	public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
		cameraPosition.position = transform.position;
		//cameraPosition.rotation = transform.rotation;
    }
}
