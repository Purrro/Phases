using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject panel; //Menu
    [SerializeField] GameObject crosshair;
    bool isPaused;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; //Free the mouse to allow the user to choose in the menu space
        isPaused = true;
        crosshair.SetActive(false);
        player.GetComponent<PlayerLookAround>().enabled = false; //Prevent the player from moving while in menu
        player.GetComponent<PlayerMovement>().enabled = false; //Prevent the player from moving while in menu
        player.transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {   
            StartGame(); //Stop || continue the game
        }
    }

    //Called when the player press the start button in the menu
    public void StartGame()
    {
        isPaused = !isPaused;
        panel.SetActive(isPaused);
        crosshair.SetActive(!isPaused);
        player.GetComponent<PlayerLookAround>().enabled = !isPaused;
        player.GetComponent<PlayerMovement>().enabled = !isPaused;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; //Ceter and hide the cursor
            Cursor.visible = false;
        }
    }
}
