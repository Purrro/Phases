using UnityEngine;

public class SafetyNetCont : MonoBehaviour
{
    public GameObject childSafety, teenSafety, adultSafety, elderSafety;
    public PlayerMovement pM;
   
    private void OnCollisionEnter(Collision collision)
    {
        if (pM.GetRoom() == "Child")
            collision.transform.position = childSafety.transform.position;
        else if (pM.GetRoom() == "Teen")
            collision.transform.position = teenSafety.transform.position;
        else if (pM.GetRoom() == "Adult")
            collision.transform.position = adultSafety.transform.position;
        else if (pM.GetRoom() == "Elder")
            collision.transform.position = elderSafety.transform.position;
    }
}
