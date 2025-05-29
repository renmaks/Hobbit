using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		var damageable = collision.gameObject.GetComponent<IDamageable>();
		Debug.Log("Collision with " + collision.gameObject.name);
		damageable?.Die();
	}
}
