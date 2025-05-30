using UnityEngine;

public class PlayerAnim
{
	private readonly Animator _animator;
	private readonly PlayerInputHandler _input;
	private readonly GroundChecker _groundChecker;

	public PlayerAnim(Animator animator, PlayerInputHandler input, GroundChecker groundChecker)
	{
		_animator = animator;
		_input = input;
		_groundChecker = groundChecker;
	}

	public void Update()
	{
		_animator.SetBool("IsRun", _input.MovementInput != Vector2.zero);
		_animator.SetBool("IsJump", _input.JumpPressed);
		_animator.SetBool("IsInAir", !_groundChecker.IsGrounded);
	}
}
