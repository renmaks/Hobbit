using UnityEngine;

public class TrigerLoad : MonoBehaviour
{
    public MusicFon_1 musicFon_1;

    public bool trigerenter = false;

    public int dsfsd = -1;
    private readonly float volumeChangeSpeed = 20f;

    private void Update()
    {
        float delta = Time.deltaTime * volumeChangeSpeed;

        if (dsfsd == 1)
        {
            musicFon_1.voluemmusic = Mathf.Clamp(musicFon_1.voluemmusic - delta, 0, 100);
            Debug.Log("No");
        }
        if (dsfsd == 0)
        {
            musicFon_1.voluemmusic = Mathf.Clamp(musicFon_1.voluemmusic + delta, 0, 100);
            Debug.Log("Yes");
        }


        if (!trigerenter)
        {
            musicFon_1.voluemmusic = Mathf.Clamp(musicFon_1.voluemmusic + delta, 0, 100);
            Debug.Log("Yes");
        }
        else
        {
            musicFon_1.voluemmusic = Mathf.Clamp(musicFon_1.voluemmusic - delta, 0, 100);
            Debug.Log("No");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigerenter = true;
            dsfsd = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigerenter = false;
            dsfsd = 0;
        }
    }
}
