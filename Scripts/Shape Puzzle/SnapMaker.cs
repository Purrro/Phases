using UnityEngine;

public class SnapMaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == tag)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
