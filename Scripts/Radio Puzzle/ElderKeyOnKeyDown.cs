using UnityEngine;

public class ElderKeyOnKeyDown : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.gameObject.SetActive(false);
        RadioStations.isAfterFirstSong = true; 
    }
}
