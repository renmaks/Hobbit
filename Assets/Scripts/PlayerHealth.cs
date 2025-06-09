using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	public void Die()
	{
		Debug.Log("Игрок погиб");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
