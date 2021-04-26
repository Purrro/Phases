using UnityEngine;

public class PosterCont : MonoBehaviour
{
    [SerializeField] GameObject painting1, painting2, poster1, poster2;

    public void ShowPosters(bool isActive)
    {  
            painting1.SetActive(!isActive);
            painting2.SetActive(!isActive);
            poster1.SetActive(isActive);
            poster2.SetActive(isActive);
    }

    public void HidePosters(GameObject poster,GameObject painting)
    {
        poster.SetActive(false);
        painting.SetActive(true);
        if (painting1.activeInHierarchy && painting2.activeInHierarchy)
        {
            RadioStations.isAfterSecondSong = true;
            RadioStations.isAfterFirstSong = false;
        }
    }
}
