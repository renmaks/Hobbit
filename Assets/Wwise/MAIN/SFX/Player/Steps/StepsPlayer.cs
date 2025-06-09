using UnityEngine;

// Класс для управления звуками шагов и прыжка игрока
public class StepsPlayer : MonoBehaviour
{
    [Header("Управление событиями")]
    public RedirectPlayer redirectPlayer; // Ссылка на скрипт управления событиями
    [Header("Поверхность")]
    public Raycast raycast; // Ссылка на скрипт с данными о поверхности и прыжке
    [Header("Поверхность рыхлая земля")]
    public TrigerStepsLooseEarth trigerStepsLooseEarth; // Ссылка на триггер рыхлой земли

    // Звуковые события Wwise
    public AK.Wwise.Event stepsTree; // Звук шагов по дереву
    public AK.Wwise.Event stepsGrass; // Звук шагов по траве
    public AK.Wwise.Event stepsLooseEarth; // Звук шагов по рыхлой земле
    public AK.Wwise.Event Jump; // Звук прыжка

    // Флаг для предотвращения повторного воспроизведения звука прыжка
    public bool reloadJump;

    // Счётчик прыжка, для приземления
    public int counterFall;

    private void Start()
    {
        // Подписка на события шагов
        if (redirectPlayer != null)
        {
            redirectPlayer.OnStepOn = PlaySteps;
        }
        else
        {
            Debug.LogWarning("RedirectPlayer не привязан в инспекторе!", this);
        }

        // Проверка, что raycast и trigerStepsLooseEarth привязаны
        if (raycast == null) Debug.LogWarning("Raycast не привязан в инспекторе!", this);
        if (trigerStepsLooseEarth == null) Debug.LogWarning("TrigerStepsLooseEarth не привязан в инспекторе!", this);
    }

    private void Update()
    {
        // Проверка для воспроизведения звука прыжка
        if (raycast != null && raycast.getKayjump && !raycast.isGrounded && reloadJump)
        {
            Jump.Post(gameObject);
            reloadJump = false;
        }

        // Сброс флага прыжка, когда игрок на земле
        if (raycast != null && raycast.isGrounded)
        {
            reloadJump = true;
        }
    }

    // Воспроизведение звука шагов
    // Воспроизведение звука шагов
    private void PlaySteps()
    {
        if (raycast == null || !raycast.getKaymoov || !raycast.isGrounded)
        {
            Debug.Log("Шаги не играют: raycast null или игрок не движется/не на земле");
            return;
        }

        // Проверяем рыхлую землю
        if (trigerStepsLooseEarth != null && trigerStepsLooseEarth.trigerStepsLooseEarth)
        {
            stepsLooseEarth.Post(gameObject);
            Debug.Log("Звук шагов: Рыхлая земля");
            return;
        }

        // Проверяем тип поверхности
        if (raycast.texture == "Grass")
        {
            stepsGrass.Post(gameObject);
            Debug.Log("Звук шагов: Трава");
        }
        else if (raycast.texture == "Tree")
        {
            stepsTree.Post(gameObject);
            Debug.Log("Звук шагов: Дерево");
        }
        else
        {
            Debug.Log($"Неизвестная поверхность: {raycast.texture}");
        }
    }
}