using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal = Vector3.up;
    private readonly float _rayLength = 1.5f;
    private readonly float _normalSmoothSpeed = 10f;

    /// <summary>
    /// Нормаль поверхности под игроком
    /// </summary>
    public Vector3 SurfaceNormal => _normal;

    /// <summary>
    /// Проекция направления движения на текущую поверхность
    /// </summary>
    public Vector3 Project(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, _normal);
    }

    private void FixedUpdate()
    {
        // Луч вниз от центра тела
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, _rayLength))
        {
            // Плавное сглаживание нормали — не прыгает резко
            _normal = Vector3.Slerp(_normal, hit.normal, _normalSmoothSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _normal = Vector3.up;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 2f);
    }
}