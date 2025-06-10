using UnityEngine;

// Класс для управления фоновой музыкой при входе/выходе игрока в триггер
public class TrigerLoad : MonoBehaviour
{


    private bool isPlayerInside; // Флаг, находится ли игрок в триггере
    private static bool anyTriggerActive; // Глобальный флаг активности триггера

    public AK.Wwise.Event homehobbitenter;
    public AK.Wwise.Event homehobbitexit;





    private void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошёл игрок и триггер ещё не активен
        if (!other.CompareTag("Player") || isPlayerInside) return;
        homehobbitenter.Post(gameObject);
        isPlayerInside = true;
        anyTriggerActive = true;
        Debug.Log("Player entered trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что вышел игрок и триггер был активен
        if (!other.CompareTag("Player") || !isPlayerInside) return;
        homehobbitexit.Post(gameObject);
        isPlayerInside = false;
        anyTriggerActive = false; // Сбрасываем глобальный флаг
        Debug.Log("Player exited trigger");
    }
}