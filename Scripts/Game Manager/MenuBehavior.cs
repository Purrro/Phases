using UnityEngine;
using UnityEngine.Audio; //Needed for AudioMixer
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    //Component belongs to EventSystem

    [SerializeField] GameObject optionsPanel;  //Assign as options panel prefab
    public GameObject[] handlers = new GameObject[3]; //The three elements of options
    [SerializeField] AudioMixer audioMixer;
    bool isFirstTimeClickStart;

    //All possible resolutions
    Vector2[] resolutions = new Vector2[] 
    { new Vector2(1024, 576),
      new Vector2(1152,648),
      new Vector2(1280,720),
      new Vector2(1366,768),
      new Vector2(1600,900),
      new Vector2(1920,1080)
    };

    //Controls binding variables
    [SerializeField] Text[] controlsTexts = new Text[5];
    [SerializeField] KeyBinding keys;

    void Start()
    {
        isFirstTimeClickStart = false;
        optionsPanel.SetActive(false);
        SetControlText();
    }

    private void StartGame()
    {
        FindObjectOfType<GameManager>().StartGame();
        if(!isFirstTimeClickStart)
        {
            FindObjectOfType<MusicManager>().ChangeSong();
            isFirstTimeClickStart = true;
        }
    }

    public void LoadOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void ShowButtons(int _handle)
    {
        foreach(GameObject handler in handlers)
        {
            handler.SetActive(false);
        }
       handlers[_handle].SetActive(true);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        optionsPanel.SetActive(false);
    }

    public void SetRes(int _choice)
    {
        Screen.SetResolution((int)resolutions[_choice].x, (int)resolutions[_choice].y, false); // Need casting from float to int
    }

  
    public void SetVolume(float _value)
    {
        audioMixer.SetFloat("Volume", _value); //"Volume" is exposed in the AudioMixer parameters
    }

    public void SetControlText(int _keyIndex, string text)
    {
        controlsTexts[_keyIndex].text = text.ToUpper(); //Makes all characters to capital letters
    }

    //Ensure the key text in options are matched to the ones in the scriptable object
    public void SetControlText()
    {
        for (int _index = 0; _index < keys.keyBindChecks.Length -1; _index++)
        {
            controlsTexts[_index].text = keys.keyBindChecks[_index].keyCode.ToString();
        }
    }

}
