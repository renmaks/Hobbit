using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;

public class JumpHandler
{
	private readonly Rigidbody _rb;
	private readonly GroundChecker _groundChecker;
	
	private const float _jumpForce = 8f;
	private float _jumpTimer;
	private bool _startTimer;
	
	public bool IsJumping { get; private set; }

	public JumpHandler(Rigidbody rb, GroundChecker checker)
	{
		_rb = rb;
		_groundChecker = checker;
	}

	public void Update()
	{
		if (_startTimer)
		{
			IsJumping = false;
			_jumpTimer += Time.deltaTime;
			Debug.Log(_jumpTimer);

			if (_jumpTimer >= 1.7f)
			{
				_jumpTimer = 0f;
				_startTimer = false;
			}
		}
			
	}
	
	/// <summary>
	/// Прыжок персонажа
	/// </summary>
	public void TryJump()
	{
		if (!_groundChecker.IsGrounded || _jumpTimer > 0f)
			return;

		var velocity = _rb.linearVelocity;
		velocity.y = _jumpForce;
		_rb.linearVelocity = velocity;
		_startTimer = true;
		IsJumping = true;
		
	}
	

	
}
