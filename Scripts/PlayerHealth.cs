using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	public void Die()
	{
		Debug.Log("Игрок погиб");
	}
}
