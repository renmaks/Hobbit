using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	//Wwise
	public Action OnDeadPlayerOn;
	public void Die()
	{
		OnDeadPlayerOn?.Invoke();

		Debug.Log("Игрок погиб");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
