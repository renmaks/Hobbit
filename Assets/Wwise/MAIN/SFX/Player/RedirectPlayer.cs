using UnityEngine;
using System;
public class RedirectPlayer : MonoBehaviour
{
    //������
    public Action OnStepOn;
    public void StepsON() => OnStepOn?.Invoke();

    //������
    public Action OnJumpOn;
    //public void JumpON() => OnJumpOn?.Invoke();
    public void JumpON()
    {
        Debug.Log("dasdad");
    }
}
