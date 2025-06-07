using UnityEngine;

// ����� ��� ���������� ������� ����� � ������ ������
public class StepsPlayer : MonoBehaviour
{
    [Header("���������� ���������")]
    public RedirectPlayer redirectPlayer; // ������ �� ������ ���������� ���������
    [Header("�����������")]
    public Raycast raycast; // ������ �� ������ � ������� � ����������� � ������
    [Header("����������� ������ �����")]
    public TrigerStepsLooseEarth trigerStepsLooseEarth;

    // �������� ������� Wwise
    public AK.Wwise.Event stepsTree;
    public AK.Wwise.Event stepsGrass;
    public AK.Wwise.Event stepsLooseEarth;// ���� ����� �� �����
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