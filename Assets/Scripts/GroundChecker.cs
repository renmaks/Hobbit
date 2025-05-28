using UnityEngine;

public class GroundChecker
{
	private readonly Transform _groundCheck;
	private readonly LayerMask _layer = LayerMask.GetMask("Ground");
	
	private const float _radius = 0.3f;

	public GroundChecker(Transform owner)
	{
		_groundCheck = owner.Find("GroundCheck");
		if (!_groundCheck)
			Debug.LogError("GroundCheck object not found!");
	}

	/// <summary>
	/// Проверка - на земле ли персонаж?
	/// </summary>
	public bool IsGrounded
	{
		get { return Physics.CheckSphere(_groundCheck.position, _radius, _layer); }
	}
}
