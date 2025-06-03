using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
	private Vector3 _normal = Vector3.up;

	/// <summary>
	/// Проекция направления движения на плоскость, перпендикулярную нормали
	/// </summary>
	public Vector3 Project(Vector3 forward)
	{
		return forward - Vector3.Dot(forward, _normal) * _normal;
	}

	/// <summary>
	/// Обновление нормали при столкновении
	/// </summary>
	private void OnCollisionStay(Collision collision)
	{
		// Выбираем первую точку касания как наиболее вероятную
		if (collision.contactCount > 0)
		{
			_normal = collision.contacts[0].normal;
		}
	}

	// По желанию: сбрасывать нормаль при отрыве от поверхности
	private void OnCollisionExit(Collision collision)
	{
		_normal = Vector3.up;
	}
}
