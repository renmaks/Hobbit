using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	[Header("InputAction reference движения персонажа")]
	[SerializeField] private InputActionReference moveAction;
	[Header("InputAction reference прыжка персонажа")]
	[SerializeField] private InputActionReference jumpAction;

	public Vector2 MovementInput { get; private set; }
	public bool JumpPressed { get; set; }

	private void OnEnable()
	{
		moveAction?.action.Enable();
		jumpAction?.action.Enable();

		// Подписка на событие
		if (jumpAction)
			jumpAction.action.started += OnJumpStarted;
	}

	private void OnDisable()
	{
		moveAction?.action.Disable();
		jumpAction?.action.Disable();

		if (jumpAction != null)
			jumpAction.action.started -= OnJumpStarted;
	}

	private void OnJumpStarted(InputAction.CallbackContext ctx)
	{
		JumpPressed = true;
	}

	public void ReadInput()
	{
		MovementInput = moveAction?.action.ReadValue<Vector2>() ?? Vector2.zero;
	}

	/// <summary>
	/// Нажат ли пробел?
	/// </summary>
	/// <returns></returns>
	public bool ConsumeJump()
	{
		if (!JumpPressed) return false;
		JumpPressed = false;
		return true;
	}
}
