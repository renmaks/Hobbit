using UnityEngine;

public class TrigerStepsLooseEarth : MonoBehaviour
{

    public bool trigerStepsLooseEarth;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошёл игрок и триггер ещё не активен
        if (other.CompareTag("Player"))
        {
            trigerStepsLooseEarth = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что вышел игрок и триггер был активен
        if (other.CompareTag("Player"))
        {
            trigerStepsLooseEarth = false;
        }
    }
}
