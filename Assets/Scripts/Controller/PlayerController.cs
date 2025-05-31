using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
	private Player _player;

	private void Awake()
	{
		var rb = GetComponent<Rigidbody>();
		var animator = GetComponentInChildren<Animator>();
		var input = GetComponent<PlayerInputHandler>();

		_player = new Player(rb, animator, transform, input);
	}

	private void Update()
	{
		_player.Update();
	}

	private void FixedUpdate()
	{
		_player.FixedUpdate();
	}
}
