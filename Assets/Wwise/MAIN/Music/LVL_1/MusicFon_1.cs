using UnityEngine;

// Класс для управления фоновой музыкой через Wwise
public class MusicFon_1 : MonoBehaviour
{
    public AK.Wwise.Event fon_1Enable; // Событие для включения фоновой музыки
    public AK.Wwise.Event fon_1Exit;   // Событие для остановки фоновой музыки
    public AK.Wwise.RTPC rTPCVolume;   // RTPC для управления громкостью

    public float volumeMusic; // Текущая громкость музыки
    public bool startMusic = true; // Флаг для запуска музыки при старте

    private void Update()
    {
        // Обновляем глобальную громкость через RTPC
        rTPCVolume.SetGlobalValue(volumeMusic);

        // Запускаем музыку один раз при старте
        if (startMusic)
        {
            fon_1Enable.Post(gameObject);
            startMusic = false;
        }
    }
}