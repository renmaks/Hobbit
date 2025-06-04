using UnityEngine;

// Класс для обработки raycast и ввода игрока
public class Raycast : MonoBehaviour
{
    public Transform Player; // Ссылка на трансформ игрока

    public bool isGrounded; // Флаг, находится ли игрок на земле
    public bool getKaymoov; // Флаг движения (WASD)
    public bool getKayjump; // Флаг прыжка (Space)
    public string texture;  // Тег поверхности под игроком

    private void Update()
    {
        // Проверка, стоит ли игрок на поверхности
        RaycastHit hit;
        Debug.DrawRay(Player.position, Vector3.down, Color.white);
        if (Physics.Raycast(Player.position, Vector3.down, out hit, 0.2f))
        {
            texture = hit.collider.gameObject.tag;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Проверка нажатия клавиш движения
        getKaymoov = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                     Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        // Проверка нажатия клавиши прыжка
        getKayjump = Input.GetKey(KeyCode.Space);
    }
}