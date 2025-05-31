using UnityEngine;

public class PlayerMovement
{
	private readonly Rigidbody _rb;
	private readonly GroundChecker _groundChecker;
	private float _fallTime;

	private const float _speed = 7f;
	private const float _rotationSpeed = 500f;
	private const float _keyThreshold = 0.001f;
	private const float _gravityMin = 1.5f;
	private const float _gravityMax = 5f;
	private const float _gravityTime = 0.7f;

	public PlayerMovement(Rigidbody rb, GroundChecker checker)
	{
		_rb = rb;
		_groundChecker = checker;
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
		var direction = Quaternion.Euler(0, yaw, 0) * new Vector3(input.x, 0, input.y);
		var targetVelocity = direction * _speed;
		targetVelocity.y = _groundChecker.IsGrounded
			? Mathf.Lerp(_rb.linearVelocity.y, 0f, 10f * Time.fixedDeltaTime)
			: _rb.linearVelocity.y;

		_rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, targetVelocity, 5f * Time.fixedDeltaTime);

		// Поворот персонажа
		var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
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

			if (!(_rb.linearVelocity.y < -1f))
				return;
			var v = _rb.linearVelocity;
			v.y = -0.1f;
			_rb.linearVelocity = v;
		}
		else
		{
			_fallTime += Time.fixedDeltaTime;
			var gravityScale = Mathf.Lerp(_gravityMin, _gravityMax, _fallTime / _gravityTime);
			_rb.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);
		}
	}
}
