using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 500f;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float jumpForce = 5f;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckRadius = 0.3f;
	[SerializeField] private LayerMask groundLayer;

	private bool _isGrounded;
	private bool _jumpRequested;

	private Vector2 _input;
	private Vector3 _direction;
	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		_rigidbody.linearDamping = 3f;
	}

	private void LateUpdate()
	{
		if (_input.sqrMagnitude < 0.001f)
		{
			_direction = Vector3.zero;
			return;
		}
		
		var yaw = Camera.main.transform.eulerAngles.y;
		_direction = Quaternion.Euler(0, yaw, 0) * new Vector3(_input.x, 0, _input.y);
		
	}

	private void FixedUpdate()
	{
		if (!_isGrounded)
			_rigidbody.AddForce(Physics.gravity * 1.6f, ForceMode.Acceleration);
		
		Jumping();

		if (_direction.sqrMagnitude < 0.001f)
			return;
		Rotation();
		Movement();
	}
	
	public void Move(InputAction.CallbackContext context)
	{
		_input = context.ReadValue<Vector2>();
	}
	
	public void Jump(InputAction.CallbackContext context)
	{
		if (_isGrounded)
			_jumpRequested = true;
	}
	
	/// <summary>
	/// Движение персонажа
	/// </summary>
	/// <returns></returns>
	private void Movement()
	{
		
		
		var targetVelocity = _direction*speed;
		targetVelocity.y = _rigidbody.linearVelocity.y;

		var velocity = Vector3.Lerp(_rigidbody.linearVelocity, targetVelocity, 5f*Time.fixedDeltaTime);
		_rigidbody.linearVelocity = velocity;
	}

	/// <summary>
	/// Поворот персонажа
	/// </summary>
	private void Rotation()
	{
		var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
	}

	/// <summary>
	/// Прыжок персонажа
	/// </summary>
	private void Jumping()
	{
		// Проверка: стоим ли на земле
		_isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

		// Прыжок
		if (!_jumpRequested)
			return;
		_jumpRequested = false;
		_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
