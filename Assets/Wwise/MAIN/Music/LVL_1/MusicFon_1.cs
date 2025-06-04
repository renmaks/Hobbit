using UnityEngine;

// ����� ��� ���������� ������� ������� ����� Wwise
public class MusicFon_1 : MonoBehaviour
{
    public AK.Wwise.Event fon_1Enable; // ������� ��� ��������� ������� ������
    public AK.Wwise.Event fon_1Exit;   // ������� ��� ��������� ������� ������
    public AK.Wwise.RTPC rTPCVolume;   // RTPC ��� ���������� ����������

    public float volumeMusic; // ������� ��������� ������
    public bool startMusic = true; // ���� ��� ������� ������ ��� ������

    private void Update()
    {
        // ��������� ���������� ��������� ����� RTPC
        rTPCVolume.SetGlobalValue(volumeMusic);

        // ��������� ������ ���� ��� ��� ������
        if (startMusic)
        {
            fon_1Enable.Post(gameObject);
            startMusic = false;
        }
    }
}