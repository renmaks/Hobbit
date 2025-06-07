using UnityEngine;

public class TrigerStepsLooseEarth : MonoBehaviour
{

    public bool trigerStepsLooseEarth;

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ����� ����� � ������� ��� �� �������
        if (other.CompareTag("Player"))
        {
            trigerStepsLooseEarth = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���������, ��� ����� ����� � ������� ��� �������
        if (other.CompareTag("Player"))
        {
            trigerStepsLooseEarth = false;
        }
    }
}
