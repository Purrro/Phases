using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] bMG = new AudioClip[4];
    AudioSource aS;
    PlayerMovement pM;
    int _songIndex;

    void Start()
    {
        _songIndex = 0;
        aS = GetComponent<AudioSource>();
        aS.clip = bMG[_songIndex];
        pM = FindObjectOfType<PlayerMovement>();
        aS.Play();
    }

    public void ChangeSong()
    {
        if (pM.GetRoom() != "Elder")
        {
            aS.clip = bMG[++_songIndex];
            aS.Play();
        }
        else
        {
            aS.clip = bMG[1];
            aS.Play();
        }
    }

    public void StopSong()
    {
        aS.Stop();
    }
}
