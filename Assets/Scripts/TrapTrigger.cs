using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		var damageable = collision.gameObject.GetComponent<IDamageable>();
		damageable?.Die();
	}
}
