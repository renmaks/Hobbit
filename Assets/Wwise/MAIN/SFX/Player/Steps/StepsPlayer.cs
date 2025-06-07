using UnityEngine;

// ����� ��� ���������� ������� ����� � ������ ������
public class StepsPlayer : MonoBehaviour
{
    [Header("���������� ���������")]
    public RedirectPlayer redirectPlayer; // ������ �� ������ ���������� ���������
    [Header("�����������")]
    public Raycast raycast; // ������ �� ������ � ������� � ����������� � ������
    [Header("����������� ������ �����")]
    public TrigerStepsLooseEarth trigerStepsLooseEarth; // ������ �� ������� ������ �����

    // �������� ������� Wwise
    public AK.Wwise.Event stepsTree; // ���� ����� �� ������
    public AK.Wwise.Event stepsGrass; // ���� ����� �� �����
    public AK.Wwise.Event stepsLooseEarth; // ���� ����� �� ������ �����
    public AK.Wwise.Event Jump; // ���� ������

    // ���� ��� �������������� ���������� ��������������� ����� ������
    public bool reloadJump;

    // ������� ������, ��� �����������
    public int counterFall;

    private void Start()
    {
        // �������� �� ������� �����
        if (redirectPlayer != null)
        {
            redirectPlayer.OnStepOn = PlaySteps;
        }
        else
        {
            Debug.LogWarning("RedirectPlayer �� �������� � ����������!", this);
        }

        // ��������, ��� raycast � trigerStepsLooseEarth ���������
        if (raycast == null) Debug.LogWarning("Raycast �� �������� � ����������!", this);
        if (trigerStepsLooseEarth == null) Debug.LogWarning("TrigerStepsLooseEarth �� �������� � ����������!", this);
    }

    private void Update()
    {
        // �������� ��� ��������������� ����� ������
        if (raycast != null && raycast.getKayjump && !raycast.isGrounded && reloadJump)
        {
            Jump.Post(gameObject);
            reloadJump = false;
        }

        // ����� ����� ������, ����� ����� �� �����
        if (raycast != null && raycast.isGrounded)
        {
            reloadJump = true;
        }
    }

    // ��������������� ����� �����
    // ��������������� ����� �����
    private void PlaySteps()
    {
        if (raycast == null || !raycast.getKaymoov || !raycast.isGrounded)
        {
            Debug.Log("���� �� ������: raycast null ��� ����� �� ��������/�� �� �����");
            return;
        }

        // ��������� ������ �����
        if (trigerStepsLooseEarth != null && trigerStepsLooseEarth.trigerStepsLooseEarth)
        {
            stepsLooseEarth.Post(gameObject);
            Debug.Log("���� �����: ������ �����");
            return;
        }

        // ��������� ��� �����������
        if (raycast.texture == "Grass")
        {
            stepsGrass.Post(gameObject);
            Debug.Log("���� �����: �����");
        }
        else if (raycast.texture == "Tree")
        {
            stepsTree.Post(gameObject);
            Debug.Log("���� �����: ������");
        }
        else
        {
            Debug.Log($"����������� �����������: {raycast.texture}");
        }
    }
}