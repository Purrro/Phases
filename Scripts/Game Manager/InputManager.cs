using System.Collections;
using UnityEngine;

public enum KeyBindingActions //All possible key actions (used in KeyBinding script)
{
    Forward,
    Back,
    Right,
    Left,
    Interact
}

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyBinding keyBinding; //Link the scriptable object
    bool isKeyPressed, isSettingKey;
    Event e; //Used to store the last key pressed
    MenuBehavior menu;

    private void Start()
    {
        menu = FindObjectOfType<MenuBehavior>();
        isKeyPressed = false;
        isSettingKey = false;
    }

    private void OnGUI() //Called everytime a key is pressed or a mouse action has occured
    {
        e = Event.current; //Store the current event that was proccesed
    }

    private void Update()
    {
        if(isSettingKey) //True when a key binding button is pressed on options
        {
            if (e.isKey) //Enure that e is assigned as a key
            {
                isKeyPressed = true; //Let the coroutine continue
            }
        }
    }
    
    //Called from PlayerMovement just as in Input.GetKey()
    public KeyCode GetKeyForAction(KeyBindingActions keyBindingAction)
    {
        foreach (KeyBinding.KeyBindCheck keyBindCheck in keyBinding.keyBindChecks)
        {
            if(keyBindCheck.keyBindingAction == keyBindingAction)
            {
                return keyBindCheck.keyCode;
            }
        }
        return KeyCode.None;
    }

    //Called from the controls menu when press on any key to setup
    public void SetKey(int action)
    {
        isSettingKey = true;
        StartCoroutine("SettingKey",action);
    }

    IEnumerator SettingKey(int action)
    {
        yield return new WaitUntil(() => isKeyPressed); //Waiting for user's input

        if(e.keyCode == KeyCode.None) //if the user's input was a character (letter)
        {
            //When using CapsLock convert to lowercase
            foreach(char character in e.character.ToString().ToLower())
            {
                e.character = character;
            }
            keyBinding.keyBindChecks[action].keyCode = (KeyCode)e.character;
            menu.SetControlText(action, e.character.ToString());
        }
        else 
        {
            keyBinding.keyBindChecks[action].keyCode = e.keyCode;
            menu.SetControlText(action, e.keyCode.ToString());
        }

        isSettingKey = false;
        isKeyPressed = false;      
    }
}

