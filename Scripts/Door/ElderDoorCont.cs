using UnityEngine;

public class ElderDoorCont : MonoBehaviour
{
    bool isClear = false;
    bool isSoundPlay = false;
    RadioInteractScript rIS;

    private void Start()
    {
        rIS = FindObjectOfType<RadioInteractScript>();
    }

    void Update()
    {
        if (ShapeEngine._correctSlot == 6)
        {
            if(!isSoundPlay)
            {
                GetComponentInChildren<AudioSource>().Play(); 
                isSoundPlay = true;
                GetComponentInChildren<Animator>().Play("DoorOpen");
                isClear = true;
                rIS.isSolved = true;
            }
        }
    }

    public bool GetIsRoomClear()
    {
        return isClear;
    }
}
