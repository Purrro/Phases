using UnityEngine;

public class TeenRoomKeyMouseDown : MonoBehaviour
{
    [SerializeField] LightSwitch lS;

    private bool isFirstTimePicked;

    private void Start()
    {
        isFirstTimePicked = false;
    }

    private void OnMouseDown()
    {
        if (!isFirstTimePicked)
        {
            isFirstTimePicked = !isFirstTimePicked;
            lS.SetLight();
        }
    }
}
