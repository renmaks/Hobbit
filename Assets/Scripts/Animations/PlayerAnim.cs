using UnityEngine;

public class PlayerAnim : MonoBehaviour {
	
	private Animator _animator;
	private PlayerInputHandler _input;

	private void Awake()
	{
		_input = GetComponent<PlayerInputHandler>();
		_animator = GetComponentInChildren<Animator>();
	}
	
	private void Update()
	{
		OnMove();
		OnJump();
	}

	private void OnMove()
	{
		_animator.SetBool("IsRun", _input.MovementInput != Vector2.zero);
	}
	
	private void OnJump()
	{
		_animator.SetBool("IsJump", _input.JumpPressed);
	}
}
