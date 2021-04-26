using UnityEngine;

[CreateAssetMenu(fileName = "Keybinding", menuName = "Keybinding")]

public class KeyBinding : ScriptableObject
{
  [System.Serializable] //Expose the class in the inspector

  public class KeyBindCheck
    {
        public KeyBindingActions keyBindingAction; //Name (enum) of action
        public KeyCode keyCode; //Coresponding keycode
    }

    public KeyBindCheck[] keyBindChecks; //Array of keys
}
