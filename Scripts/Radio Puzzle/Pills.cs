using UnityEngine;

public class Pills : MonoBehaviour
{
    private void OnMouseDown()
    {
        RadioStations.isTakePill = true;
        Destroy(this.gameObject);
    }
}
