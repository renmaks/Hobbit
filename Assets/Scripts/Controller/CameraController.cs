using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	[Header("Объект игрока")]
	[SerializeField] private Rigidbody playerRigidbody;
	[Header("Чувствительность мыши")]
	[SerializeField] private MouseSensitivity mouseSensitivity;
	[Header("Угол камеры")]
	[SerializeField] private CameraAngle cameraAngle;
	[Header("Множитель плавности движения камеры")]
	[SerializeField] private float followSmoothTime = 0.05f;
	[Header("Дистанция камеры от игрока")]
	[SerializeField] private float cameraDistance = 3f;
	[Header("Высота камеры от игрока")]
	[SerializeField] private float cameraHeight = 2f;
	[Header("Слои, которые камера пересекать не должна")]
	[SerializeField] private LayerMask collisionMask;

	private Vector2 _input;
	private float _yaw;
	private float _pitch;
	private Vector3 _currentVelocity;
	private Transform _cameraTransform;

	private void Awake()
	{
		if (Camera.main) _cameraTransform = Camera.main.transform;
	}

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
		// Поворот rig по мыши
		transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);

		// Плавное перемещение rig к позиции игрока
		transform.position = Vector3.SmoothDamp(
			transform.position,
			playerRigidbody.position,
			ref _currentVelocity,
			followSmoothTime
		);

		UpdateCameraPosition();
	}

	private void UpdateCameraPosition()
	{
		// Точка отсчёта: transform.position + смещение вверх
		var pivotPoint = transform.position + Vector3.up * cameraHeight;

		// Направление — назад от поворота камеры
		var desiredCameraOffset = -transform.forward * cameraDistance;
		var desiredPosition = pivotPoint + desiredCameraOffset;

		if (Physics.Raycast(pivotPoint, desiredCameraOffset.normalized, out var hit, cameraDistance, collisionMask))
		{
			_cameraTransform.position = pivotPoint + desiredCameraOffset.normalized * (hit.distance - 0.2f);
		}
		else
		{
			_cameraTransform.position = desiredPosition;
		}

		_cameraTransform.LookAt(pivotPoint);

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
