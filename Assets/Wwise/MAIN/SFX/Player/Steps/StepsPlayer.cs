using UnityEngine;

// ����� ��� ���������� ������� ����� � ������ ������
public class StepsPlayer : MonoBehaviour
{
    [Header("���������� ���������")]
    public RedirectPlayer redirectPlayer; // ������ �� ������ ���������� ���������
    [Header("�����������")]
    public Raycast raycast; // ������ �� ������ � ������� � ����������� � ������

    // �������� ������� Wwise
    public AK.Wwise.Event stepsTree;
    public AK.Wwise.Event stepsGrass; // ���� ����� �� �����
    public AK.Wwise.Event Jump; // ���� ������

    // ���� ��� �������������� ���������� ��������������� ����� ������
    public bool reloadJump;

    // ������� ������, ��� �����������
    public int counterFall;

    private void Start()
    {
        // �������� �� ������� ����� � ������
        redirectPlayer.OnStepOn = PlaySteps;
    }

    private void Update()
    {
        // �������� ��� ��������������� ����� ������
        if (raycast.getKayjump && !raycast.isGrounded && reloadJump)
        {
            Jump.Post(gameObject);
            reloadJump = false;
        }

        // ����� ����� ������, ����� ����� �� �����
        if (raycast.isGrounded)
        {
            reloadJump = true;
        }

        
            
        
    }

    // ��������������� ����� �����
    private void PlaySteps()
    {
        if (raycast.getKaymoov && raycast.isGrounded)
        {
            if (raycast.texture == "Grass")
            {
                stepsGrass.Post(gameObject);
            }

            if (raycast.texture == "Tree")
            {
                stepsTree.Post(gameObject);
            }
        }
    }

}