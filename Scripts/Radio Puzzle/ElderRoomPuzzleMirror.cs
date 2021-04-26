using UnityEngine;

public class ElderRoomPuzzleMirror : MonoBehaviour
{
    [SerializeField] Transform mirror;
    [SerializeField] Transform playerCam;
    
    [SerializeField] GameObject elderRoomKey;

    [SerializeField] AudioClip adultMusic; // Really guys. it's R rated

    private void Start()
    {
        elderRoomKey.SetActive(false);
    }

    void Update()
    {
        Vector3 targetDir = mirror.position - playerCam.position;
        float _angle = Vector3.Angle(targetDir, playerCam.forward);
        float _dis = Vector3.Distance(mirror.position, playerCam.position);

        // Check the angle between the player and the mirror object.
        if (_angle > 40f &&_angle < 60f && _dis < 1.5f && FindObjectOfType<RadioStations>().GetCurrentSong() == adultMusic)
        {
            ShowKey();  
        }
    }

    public void ShowKey()
    {
        elderRoomKey.SetActive(true);
    }
}
