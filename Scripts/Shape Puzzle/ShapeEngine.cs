using UnityEngine;

public class ShapeEngine : MonoBehaviour
{
    public static int _correctSlot = 0;
    public AudioSource door;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == name)
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            _correctSlot++;
            gameObject.SetActive(false);
            
            if (_correctSlot == 3)
            {
                door.Play();
            }
        }
    }
}
