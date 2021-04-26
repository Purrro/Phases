using UnityEngine;

public class PaintingContElder : MonoBehaviour
{
    public GameObject painting;

    private void OnMouseDown()
    {
        FindObjectOfType<PosterCont>().HidePosters(gameObject, painting);
    }
}
