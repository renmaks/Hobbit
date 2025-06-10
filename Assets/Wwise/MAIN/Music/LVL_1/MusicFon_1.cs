using UnityEngine;

// ����� ��� ���������� ������� ������� ����� Wwise
public class MusicFon_1 : MonoBehaviour
{
    //Dead
    public PlayerHealth DeadPlayer;

    public AK.Wwise.Event fon_1Enable; // ������� ��� ��������� ������� ������
    public AK.Wwise.Event fon_1Exit;   // ������� ��� ��������� ������� ������



    public float volumeMusic; // ������� ��������� ������
    public bool startMusic = true; // ���� ��� ������� ������ ��� ������

    public void Start()
    {
        if (DeadPlayer == null)
        {
            DeadPlayer = GameObject.Find("Player")?.GetComponent<PlayerHealth>();
        }
        

        DeadPlayer.OnDeadPlayerOn = DeadPlayer_;
    }

    private void Update()
    {


        // ��������� ������ ���� ��� ��� ������
        if (startMusic)
        {
            fon_1Enable.Post(gameObject);
            startMusic = false;
        }

    }

    public void DeadPlayer_()
    {
        fon_1Exit.Post(gameObject);
    }
}