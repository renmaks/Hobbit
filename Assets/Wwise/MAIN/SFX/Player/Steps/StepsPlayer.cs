using UnityEngine;

// Класс для управления звуками шагов и прыжка игрока
public class StepsPlayer : MonoBehaviour
{
    [Header("Управления событиями")]
    public RedirectPlayer redirectPlayer; // Ссылка на скрипт управления событиями
    [Header("Поверхность")]
    public Raycast raycast; // Ссылка на скрипт с данными о поверхности и прыжке
    [Header("Поверхность рыхлая земля")]
    public TrigerStepsLooseEarth trigerStepsLooseEarth;

    // Звуковые события Wwise
    public AK.Wwise.Event stepsTree;
    public AK.Wwise.Event stepsGrass;
    public AK.Wwise.Event stepsLooseEarth;// Звук шагов по траве
    public AK.Wwise.Event Jump; // Звук прыжка

    // Флаг для предотвращения повторного воспроизведения звука прыжка
    public bool reloadJump;

    // Счётчик прыжка, для приземления
    public int counterFall;

    private void Start()
    {
        // Подписка на события шагов и прыжка
        redirectPlayer.OnStepOn = PlaySteps;
    }

    private void Update()
    {
        // Проверка для воспроизведения звука прыжка
        if (raycast.getKayjump && !raycast.isGrounded && reloadJump)
        {
            Jump.Post(gameObject);
            reloadJump = false;
        }

        // Сброс флага прыжка, когда игрок на земле
        if (raycast.isGrounded)
        {
            reloadJump = true;
        }

        
            
        
    }

    // Воспроизведение звука шагов
    private void PlaySteps()
    {
        if (raycast.getKaymoov && raycast.isGrounded)
        {
            if (raycast.texture == "Grass" || trigerStepsLooseEarth.trigerStepsLooseEarth == false)
            {
                stepsGrass.Post(gameObject);
            }

            if (trigerStepsLooseEarth.trigerStepsLooseEarth == true)
            {
                stepsLooseEarth.Post(gameObject);
            }

            if (raycast.texture == "Tree")
            {
                stepsTree.Post(gameObject);
            }

        }
    }

}