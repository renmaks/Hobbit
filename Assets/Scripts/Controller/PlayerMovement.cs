using UnityEngine;

public class PlayerMovement
{
	private readonly Rigidbody _rb;
	private readonly GroundChecker _groundChecker;
	private readonly SurfaceSlider _surfaceSlider;
	private float _fallTime;
	private Vector3 _lastDirection = Vector3.forward;


	private const float _speed = 7f;
	private const float _rotationSpeed = 500f;
	private const float _keyThreshold = 0.001f;
	private const float _gravityMin = 1.5f;
	private const float _gravityMax = 5f;
	private const float _gravityTime = 0.7f;

	public PlayerMovement(Rigidbody rb, GroundChecker checker, SurfaceSlider slider)
	{
		_rb = rb;
		_groundChecker = checker;
		_surfaceSlider = slider;
	}

	/// <summary>
	/// Движение персонажа
	/// </summary>
	/// <param name="input"></param>
	public void Move(Vector2 input)
	{
		if (input.sqrMagnitude < _keyThreshold)
			return;

		// Направление от камеры
		var yaw = Camera.main.transform.eulerAngles.y;
		var inputDirection = new Vector3(input.x, 0f, input.y);
		var worldDirection = Quaternion.Euler(0, yaw, 0) * inputDirection;

		// Спроецированное направление по поверхности
		var slopeDirection = _surfaceSlider.Project(worldDirection).normalized;

		// Задание скорости (оставляем Y как есть)
		var targetVelocity = slopeDirection * _speed;
		targetVelocity.y = _rb.linearVelocity.y;

		// Плавное движение
		_rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, targetVelocity, 5f * Time.fixedDeltaTime);

		_lastDirection = Vector3.Slerp(_lastDirection, slopeDirection, 10f * Time.fixedDeltaTime);
		
		// Поворот по направлению
		if (!(slopeDirection.sqrMagnitude > _keyThreshold))
			return;
		var targetRotation = Quaternion.LookRotation(_lastDirection, Vector3.up);
		_rb.rotation = Quaternion.RotateTowards(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
	}

	/// <summary>
	/// Смена гравитации при падении
	/// </summary>
	public void ApplyGravity()
	{
		if (_groundChecker.IsGrounded)
		{
			_fallTime = 0f;
		}
		else
		{
			_fallTime += Time.fixedDeltaTime;
			var gravityScale = Mathf.Lerp(_gravityMin, _gravityMax, _fallTime / _gravityTime);
			_rb.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);
		}
	}
}
