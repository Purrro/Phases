using UnityEngine;

/* 
 * This script is only for testing purposes.
 * Any use of any of this scripts components is considered cheating
 * BEWARE!!!
 */

public class TransferToRoom : MonoBehaviour 

    // Cheat code for devs to jump between rooms
{
    [SerializeField] MusicManager mM;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = new Vector3(2, 1, -5);
            transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = new Vector3(12, 1, -15);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mM.ChangeSong();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = new Vector3(22, 1, -25);
            transform.localScale = new Vector3(0.5f, 0.75f, 0.5f);
            mM.ChangeSong();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = new Vector3(32, 1, -5);
            transform.localScale = new Vector3(0.5f, 0.75f, 0.5f);
            mM.StopSong();
        }
    }
}
