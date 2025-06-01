using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	[Header("Объект игрока")]
	[SerializeField] private Rigidbody playerRigidbody;

	[Header("Чувствительность мыши")]
	[SerializeField] private MouseSensitivity mouseSensitivity;

	[Header("Ограничения угла наклона камеры")]
	[SerializeField] private CameraAngle cameraAngle;

	[Header("Плавность следования")]
	[SerializeField] private float followSmoothTime = 0.05f;

	private Vector2 _input;
	private float _yaw;
	private float _pitch;
	private Vector3 _currentVelocity;

	/// <summary>
	/// Обработка движения мыши
	/// </summary>
	public void Look(InputAction.CallbackContext context)
	{
		_input = context.ReadValue<Vector2>();
	}

	private void Update()
	{
		_yaw += _input.x * mouseSensitivity.horizontal * Time.deltaTime;
		_pitch -= _input.y * mouseSensitivity.vertical * Time.deltaTime;
		_pitch = Mathf.Clamp(_pitch, cameraAngle.min, cameraAngle.max);
	}

	private void LateUpdate()
	{
		// Плавное перемещение rig к позиции игрока
		transform.position = Vector3.SmoothDamp(
			transform.position,
			playerRigidbody.position,
			ref _currentVelocity,
			followSmoothTime
		);

		// Поворот rig по мыши
		transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
	}
}

[System.Serializable]
public struct MouseSensitivity
{
	public float horizontal;
	public float vertical;
}

[System.Serializable]
public struct CameraAngle
{
	public float min;
	public float max;
}
