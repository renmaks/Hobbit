using UnityEngine;

// ����� ��� ��������� raycast � ����� ������
public class Raycast : MonoBehaviour
{
    public Transform Player; // ������ �� ��������� ������

    public bool isGrounded; // ����, ��������� �� ����� �� �����
    public bool getKaymoov; // ���� �������� (WASD)
    public bool getKayjump; // ���� ������ (Space)
    public string texture;  // ��� ����������� ��� �������

    private void Update()
    {
        // ��������, ����� �� ����� �� �����������
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

        // �������� ������� ������ ��������
        getKaymoov = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                     Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        // �������� ������� ������� ������
        getKayjump = Input.GetKey(KeyCode.Space);
    }
}