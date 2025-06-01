using UnityEngine;

public class PlayerMovement
{
	private readonly Rigidbody _rb;
	private readonly GroundChecker _groundChecker;
	private readonly SurfaceSlider _surfaceSlider;
	private float _fallTime;
	private Vector3 _smoothedDirection = Vector3.forward;


	private const float _speed = 7f;
	private const float _rotationSpeed = 500f;
	private const float _keyThreshold = 0.001f;

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

		var yaw = Camera.main.transform.eulerAngles.y;
		var inputDirection = new Vector3(input.x, 0f, input.y);
		var worldDirection = Quaternion.Euler(0, yaw, 0) * inputDirection;

		var slopeDirection = _surfaceSlider.Project(worldDirection).normalized;
		_smoothedDirection = Vector3.Slerp(_smoothedDirection, slopeDirection, 6f * Time.fixedDeltaTime);


		var verticalSpeed = _rb.linearVelocity.y;

		var targetHorizontal = _smoothedDirection  * _speed;

		var currentHorizontal = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

		var lerpedHorizontal = Vector3.Lerp(currentHorizontal, targetHorizontal, 5f * Time.fixedDeltaTime);

		var finalVelocity = new Vector3(lerpedHorizontal.x, verticalSpeed, lerpedHorizontal.z);

		_rb.linearVelocity = finalVelocity;

		// Поворот по направлению движения
		if (!(slopeDirection.sqrMagnitude > _keyThreshold))
			return;
		var targetRotation = Quaternion.LookRotation(_smoothedDirection , Vector3.up);
		_rb.rotation = Quaternion.RotateTowards(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);

		switch (_groundChecker.IsGrounded)
		{
			// Прижатие вниз — только если в воздухе, чтобы не прилипал в прыжке
			case false:
				_rb.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
				break;
			// Если застрял (движется, но скорость почти нулевая) — дать ускорение
			case true when input.sqrMagnitude > 0.01f:
			{
				var horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
				if (horizontalVelocity.sqrMagnitude < 0.05f)
				{
					_rb.AddForce(_smoothedDirection * (_speed * 1.5f), ForceMode.Acceleration);
				}
				break;
			}
		}
	}
	
	/// <summary>
	/// Смена гравитации при падении
	/// </summary>
	public void ApplyGravity()
	{
		if (_groundChecker.IsGrounded)
		{
			_fallTime = 0f;
			return;
		}

		_fallTime += Time.fixedDeltaTime;

		var velocity = _rb.linearVelocity;

		// Простейшее ускорение вниз вручную
		velocity.y += Physics.gravity.y * Time.fixedDeltaTime;

		_rb.linearVelocity = velocity;
	}
}
