using UnityEngine;

// Класс для управления фоновой музыкой через Wwise
public class MusicFon_1 : MonoBehaviour
{
    //Dead
    public PlayerHealth DeadPlayer;

    public AK.Wwise.Event fon_1Enable; // Событие для включения фоновой музыки
    public AK.Wwise.Event fon_1Exit;   // Событие для остановки фоновой музыки



    public float volumeMusic; // Текущая громкость музыки
    public bool startMusic = true; // Флаг для запуска музыки при старте

    public void Start()
    {
        if (DeadPlayer == null)
        {
            DeadPlayer = GameObject.Find("Player")?.GetComponent<PlayerHealth>();
        }
        

        DeadPlayer.OnDeadPlayerOn = DeadPlayer_;
    }

    private void Update()
    {


        // Запускаем музыку один раз при старте
        if (startMusic)
        {
            fon_1Enable.Post(gameObject);
            startMusic = false;
        }

    }

    public void DeadPlayer_()
    {
        fon_1Exit.Post(gameObject);
    }
}