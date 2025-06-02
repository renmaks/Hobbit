using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Transform Player;

    public bool isGrounded;

    public bool getKaymoov;
    public bool getKayjump;

    public string texture;
    public void Update()
    {
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

        //проверка нажатие кнопок

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            getKaymoov = true;
        else
        {
            getKaymoov = false;
        }
        if (Input.GetKey(KeyCode.Space))
            getKayjump = true;
        else
        {
            getKayjump = false;
        }

    }
}
