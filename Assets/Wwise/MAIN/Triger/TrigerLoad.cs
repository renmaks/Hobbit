using UnityEngine;

// Класс для управления фоновой музыкой при входе/выходе игрока в триггер
public class TrigerLoad : MonoBehaviour
{
    [Header("Общая фоновая музыка")]
    public MusicFon_1 musicFon_1; // Ссылка на компонент фоновой музыки

    private bool isPlayerInside; // Флаг, находится ли игрок в триггере
    private static bool anyTriggerActive; // Глобальный флаг активности триггера

    private const float speed = 10f; // Скорость изменения громкости

    private void Start()
    {
        // Ищем компонент MusicFon_1, если не задан в инспекторе
        if (musicFon_1 == null)
        {
            musicFon_1 = GameObject.Find("MusicFon")?.GetComponent<MusicFon_1>();
            if (musicFon_1 == null)
            {
                Debug.LogWarning("MusicFon_1 не найден!");
            }
        }
    }

    private void Update()
    {
        // Плавное изменение громкости музыки
        float delta = Time.deltaTime * speed;
        float target = anyTriggerActive ? 10f : 100f;
        musicFon_1.volumeMusic = Mathf.MoveTowards(musicFon_1.volumeMusic, target, delta);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошёл игрок и триггер ещё не активен
        if (!other.CompareTag("Player") || isPlayerInside) return;

        isPlayerInside = true;
        anyTriggerActive = true;
        Debug.Log("Player entered trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что вышел игрок и триггер был активен
        if (!other.CompareTag("Player") || !isPlayerInside) return;

        isPlayerInside = false;
        anyTriggerActive = false; // Сбрасываем глобальный флаг
        Debug.Log("Player exited trigger");
    }
}