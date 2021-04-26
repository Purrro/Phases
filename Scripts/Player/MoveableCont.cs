using UnityEngine;

public class MoveableCont : MonoBehaviour
{
    public bool isCollided;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Player")
        isCollided = true;
    }
}
