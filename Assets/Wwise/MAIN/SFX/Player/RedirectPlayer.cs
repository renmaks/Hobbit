using UnityEngine;
using System;

// ����� ��� ���������� ��������� ��������� ������
public class RedirectPlayer : MonoBehaviour
{
    // ������� ��� ����� �����
    public Action OnStepOn;
    public void StepsON() => OnStepOn?.Invoke();

}