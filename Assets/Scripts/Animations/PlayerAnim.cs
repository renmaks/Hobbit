using UnityEngine;

public class PlayerAnim
{
	private readonly Animator _animator;
	private readonly PlayerInputHandler _input;
	private readonly GroundChecker _groundChecker;
	private readonly JumpHandler _jumpHandler;

	public PlayerAnim(Animator animator, PlayerInputHandler input, GroundChecker groundChecker, JumpHandler jumpHandler)
	{
		_animator = animator;
		_input = input;
		_groundChecker = groundChecker;
		_jumpHandler = jumpHandler;
	}

	public void Update()
	{
		_animator.SetBool("IsRun", _input.MovementInput != Vector2.zero);
		_animator.SetBool("IsJump", _jumpHandler.IsJumping);
		_animator.SetBool("IsInAir", !_groundChecker.IsGrounded);
	}
}
