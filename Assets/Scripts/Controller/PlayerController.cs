using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	private PlayerInputHandler _input;
	private PlayerMovement _motor;
	private JumpHandler _jumpHandler;

	private void Awake()
	{
		var rb = GetComponent<Rigidbody>();
		_input = GetComponent<PlayerInputHandler>();
		var groundChecker = new GroundChecker(transform);
		_motor = new PlayerMovement(rb, groundChecker);
		_jumpHandler = new JumpHandler(rb, groundChecker);
	}

	private void Update()
	{
		_input.ReadInput();
	}

	private void FixedUpdate()
	{
		_motor.Move(_input.MovementInput);
		_motor.ApplyGravity();

		if (_input.ConsumeJump())
			_jumpHandler.TryJump();
	}
}
