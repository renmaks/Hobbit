using UnityEngine;
using System;

// Класс для управления звуковыми событиями игрока
public class RedirectPlayer : MonoBehaviour
{
    // Событие для звука шагов
    public Action OnStepOn;
    public void StepsON() => OnStepOn?.Invoke();

}