using UnityEngine;

public class AdultDoor : MonoBehaviour
{
    bool isClear;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "AdultRoomKey")
        {
            GetComponent<AudioSource>().Play();
            GetComponentInChildren<Animator>().Play("DoorOpen");
            Destroy(collision.transform.gameObject);
            isClear = true;
        }
    }

    public bool GetIsRoomClear()
    {
        return isClear;
    }
}
