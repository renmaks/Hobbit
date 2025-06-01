using UnityEngine;

public class JumpHandler
{
	private readonly Rigidbody _rb;
	private readonly GroundChecker _groundChecker;
	
	private const float _jumpForce = 8f;

	public JumpHandler(Rigidbody rb, GroundChecker checker)
	{
		_rb = rb;
		_groundChecker = checker;
	}

	/// <summary>
	/// Прыжок персонажа
	/// </summary>
	public void TryJump()
	{
		if (!_groundChecker.IsGrounded)
			return;

		var velocity = _rb.linearVelocity;
		velocity.y = _jumpForce;
		_rb.linearVelocity = velocity;
	}

	
}
