using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Player's Attributes
    [SerializeField] float _speed;
    public AudioClip[] audioClips = new AudioClip[2];
    bool isRightStep; //switch footsteps sounds
    float _stepRate = 0;
    float _x, _z;
 
    private string currentLevel;

    [SerializeField] InputManager inputManager;

    void Start()
    {
        isRightStep = false;
        _speed = 0.7f;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Check with the InputManager what key was pressed
        if(Input.GetKey(inputManager.GetKeyForAction(KeyBindingActions.Forward)))  _z = 1;
        else if (Input.GetKey(inputManager.GetKeyForAction(KeyBindingActions.Back))) _z = -1;
        else _z = 0;

        if (Input.GetKey(inputManager.GetKeyForAction(KeyBindingActions.Left))) _x = -1;
        else if (Input.GetKey(inputManager.GetKeyForAction(KeyBindingActions.Right))) _x = 1;
        else _x = 0;

        Vector3 move = transform.right * _x + transform.forward * _z;
        transform.position += move * _speed * Time.deltaTime;

        PlayStepSound(_x, _z);
    }

    //Control the rate and sound of the player's footsteps
    private void PlayStepSound(float _x, float _z)
    {
        _stepRate -= Time.deltaTime;

        if (_x != 0 && _stepRate <= 0 || _z != 0 && _stepRate <= 0)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (isRightStep)
            {
                audioSource.PlayOneShot(audioClips[0]);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[1]);
            }
            isRightStep = !isRightStep;
            _stepRate = 0.3f;
        }
    }

    //Check player's current room
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ChildFloor")
        {
            _speed = 0.7f;
            currentLevel = "Child";
        }
           
        if (collision.transform.tag == "TeenFloor")
        {
            _speed = 1.5f;
            currentLevel = "Teen";
        }
            
        else if (collision.transform.tag == "AdultFloor")
        {
            _speed = 2;
            currentLevel = "Adult";
        }

        else if (collision.transform.tag == "ElderFloor")
        {
            _speed = 1.5f;
            currentLevel = "Elder";
        }
    }

    // Getter for currentLevel
    public string GetRoom()
    {
        return currentLevel;
    }
}