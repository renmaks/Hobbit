using UnityEngine;
using System;
public class RedirectPlayer : MonoBehaviour
{
    //ходьба
    public Action OnStepOn;
    public void StepsON() => OnStepOn?.Invoke();

    //прыжок
    public Action OnJumpOn;
    //public void JumpON() => OnJumpOn?.Invoke();
    public void JumpON()
    {
        Debug.Log("dasdad");
    }
}
