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
	private float _fallTime;

	const float _linearDampingValue = 3f;
	const float _keyShift = 0.001f;
	const float _gravityScaleMin = 1.5f;
	const float _gravityScaleMax = 5f;
	const float _gravityScaleTime = 0.7f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | 
		                         RigidbodyConstraints.FreezeRotationZ | 
		                         RigidbodyConstraints.FreezeRotationY;
		_rigidbody.linearDamping = _linearDampingValue;
	}

	private void LateUpdate()
	{
		if (_input.sqrMagnitude < _keyShift)
		{
			_direction = Vector3.zero;
			return;
		}
		
		var yaw = Camera.main.transform.eulerAngles.y;
		_direction = Quaternion.Euler(0, yaw, 0) * new Vector3(_input.x, 0, _input.y);
		
	}

	private void FixedUpdate()
	{
		// Проверка: стоим ли на земле
		_isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
		
		if (_isGrounded && _jumpRequested)
			Jumping();
			/*_rigidbody.AddForce(Physics.gravity * 1.6f, ForceMode.Acceleration);*/
		
			if (_isGrounded)
			{
				_fallTime = 0f; // сброс таймера при приземлении
			}
			else
			{
				_fallTime += Time.fixedDeltaTime;

				// Пример нарастания гравитации: от 1.0 до 2.5 в течение 1 секунды
				var gravityScale = Mathf.Lerp(_gravityScaleMin, _gravityScaleMax, _fallTime / _gravityScaleTime);
				_rigidbody.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);
			}
		
		// "Гашение" скорости при приземлении
		if (_isGrounded && _rigidbody.linearVelocity.y < -1f)
		{
			var v = _rigidbody.linearVelocity;
			v.y = -0.1f;
			_rigidbody.linearVelocity = v;
		}
		

		if (_direction.sqrMagnitude < _keyShift)
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
		var targetVelocity = _direction * speed;

		// 🧠 Смягчаем вертикальное движение при соприкосновении
		// Плавно приближаемся к 0, а не резко
		targetVelocity.y = _isGrounded ? Mathf.Lerp(_rigidbody.linearVelocity.y, 0f, 10f * Time.fixedDeltaTime) : _rigidbody.linearVelocity.y;

		// Без изменения: сглаживаем скорость
		var velocity = Vector3.Lerp(_rigidbody.linearVelocity, targetVelocity, 5f * Time.fixedDeltaTime);
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
		/*// Проверка: стоим ли на земле
		_isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);*/

		// Прыжок
		if (!_jumpRequested)
			return;
		_jumpRequested = false;
		_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
