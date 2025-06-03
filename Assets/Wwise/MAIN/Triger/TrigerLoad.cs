using UnityEngine;

public class TrigerLoad : MonoBehaviour
{
	[Header("Общая фоновая музыка")]
	public MusicFon_1 musicFon_1;
	
	private bool isPlayerInside;

	private static bool anyTriggerActive;

	const float speed = 10f;

	private void Update()
	{
		var delta = Time.deltaTime * speed;
		var target = anyTriggerActive ? 0f : 100f;
		musicFon_1.voluemmusic = Mathf.MoveTowards(musicFon_1.voluemmusic, target, delta);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player") || isPlayerInside)
			return;
		isPlayerInside = true;
		anyTriggerActive = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player") || !isPlayerInside)
			return;
		isPlayerInside = false;
		// Только если **нет других активных триггеров**, возвращаем фон
		// (но мы не считаем, а просто обнуляем глобальный флаг — по твоему условию)
		anyTriggerActive = false;
	}
}
