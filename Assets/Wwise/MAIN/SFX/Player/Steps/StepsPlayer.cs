using UnityEngine;

public class StepsPlayer : MonoBehaviour
{
    public RedirectPlayer redirectPlayer;
    public Raycast raycast;

    public AK.Wwise.Event stepsgrass;
    public AK.Wwise.Event jump;


    //прыжок
    public bool reloadjump;
    private void Start()
    {
        redirectPlayer.OnStepOn = PlaySteps;
        redirectPlayer.OnJumpOn = PlayJump;
    }

    private void PlaySteps()
    {
        if (raycast.getKaymoov == true && raycast.isGrounded == true)
        {
            stepsgrass.Post(gameObject);
        }
        
    }


    private void Update()
    {
        

        if (raycast.getKayjump == true && raycast.isGrounded == false && reloadjump == true)
        {
            jump.Post(gameObject);
            reloadjump = false;
        }
        
        if(raycast.isGrounded == true)
        {
            reloadjump = true;
        }

    }
    private void PlayJump()
    {
        
        
    }
}
