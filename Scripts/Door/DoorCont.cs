using UnityEngine;

public class DoorCont : MonoBehaviour
{
    bool isClear = false;

    void Update()
    {
      if(ShapeEngine._correctSlot == 3) //this will check whether the child room puzzle is done
        {
            GetComponentInChildren<Animator>().Play("DoorOpen");
            isClear = true;
        }
    }
    public bool GetIsRoomClear()
    {
        return isClear;
    }
}
