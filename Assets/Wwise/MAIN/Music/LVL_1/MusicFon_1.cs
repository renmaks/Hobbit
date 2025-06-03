using UnityEngine;

public class MusicFon_1 : MonoBehaviour
{
    public AK.Wwise.Event fon_1Eneble;
    public AK.Wwise.Event fon_1Exit;

    public AK.Wwise.RTPC rTPCvoluem;

    public float voluemmusic;

    public bool StartMusic = true;
    public void Update()
    {
        rTPCvoluem.SetGlobalValue(voluemmusic);


        if (StartMusic == true)
        {
            fon_1Eneble.Post(gameObject);
            StartMusic = false;
        }

    }

}
