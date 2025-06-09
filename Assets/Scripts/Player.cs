using UnityEngine;

public class Player
{
	private readonly PlayerInputHandler _input;
	private readonly PlayerMovement _movement;
	private readonly JumpHandler _jump;
	private readonly PlayerAnim _anim;

	public Player(Rigidbody rb, Animator animator, Transform transform, PlayerInputHandler input, SurfaceSlider slider)
	{
		_input = input;
		var groundChecker = new GroundChecker(transform);
		_movement = new PlayerMovement(rb, groundChecker, slider);
		_jump = new JumpHandler(rb, groundChecker);
		_anim = new PlayerAnim(animator, _input, groundChecker, _jump);
	}

	public void Update()
	{
		_input.ReadInput();
		_anim.Update();
		_jump.Update();
	}

	public void FixedUpdate()
	{
		_movement.Move(_input.MovementInput);
		
		if (_input.ConsumeJump())
			_jump.TryJump();
		_movement.ApplyGravity(); 
	
	}
}
