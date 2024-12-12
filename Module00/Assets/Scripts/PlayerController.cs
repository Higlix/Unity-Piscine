using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	public float moveSpeed;

	public float groundDrag;

	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump = true;

	[Header("Keybinds")]
	public KeyCode jumpKey = KeyCode.Space;

	[Header("Ground Check")]
	public float playerHeight;
	public LayerMask whatIsGround;
	bool grounded;

	[Header("Player Transform")]
	public Transform orientation;

	float horizontalInput;
	float verticalInput;

	[Header("Death Layer ID")]
	public int lavaLayerID = 11;

	Vector3 moveDirection;

	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}

	private void FixedUpdate()
	{
		MovePlayer();
	}

	private void Update()
	{
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

		HandleInput();
		SpeedControl();

		if (grounded)
		{
			rb.linearDamping = groundDrag;
		}
		else
		{
			rb.linearDamping = 0;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == lavaLayerID)
		{
			killPlayer();
		}
	}

	private void killPlayer()
	{
		Debug.Log("GAME OVER");
		Destroy(gameObject);
	}

	private void HandleInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (Input.GetKey(jumpKey) && readyToJump && grounded)
		{
			readyToJump = false;
			Jump();
			Invoke(nameof(ResetJump), jumpCooldown);
		}
	}

	private void MovePlayer()
	{
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

		if (grounded)
			rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
		else if (!grounded)
			rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
	}

	private void SpeedControl()
	{
		Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

		if (flatVel.magnitude > moveSpeed)
		{
			Vector3 limitedVel = flatVel.normalized * moveSpeed;
			rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, rb.linearVelocity.z);
		}
	}

	private void Jump()
	{
		rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}

	private void ResetJump()
	{
		readyToJump = true;
	}
}