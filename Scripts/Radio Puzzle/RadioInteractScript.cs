using UnityEngine;

public class RadioInteractScript : MonoBehaviour
{
    [SerializeField] GameObject knob;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerLookAround playerLookAround;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject cameraLockPos;

    private Vector3 ogCamPos;
    private Quaternion ogCamRotation;

    private bool isHolding;

    private float _xMouse;
    private float _rotationSpeed;

    
    void Start()
    {
        isHolding = false;
        _rotationSpeed = 300f;
        isSolved = false;
    }

    private void OnMouseDown()
    {
        float _dist = Vector3.Distance(transform.position,playerMovement.transform.position); // Check player distance from object

        if (_dist <= 3f && !isSolved) // if distance
        {
            isHolding = true;
            playerMovement.enabled = false;
            playerLookAround.enabled = false; // Camera is static. no rotation.

            // store original camera position and rotation
            ogCamPos = playerCamera.transform.position;
            ogCamRotation = playerCamera.transform.rotation;

            // Move Camera to lamp position and rotate lamp to match
            playerCamera.transform.position = cameraLockPos.transform.position; // Change to look AT radio
            playerCamera.transform.rotation = cameraLockPos.transform.rotation;
        }
    }

    private void Update()
    {
        if (isHolding) // if hold item for more then 1 sec
        {
            // Add change knob rotation when moving mouse left <-> right
            _xMouse += Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
            _xMouse = Mathf.Clamp(_xMouse, -170, 170);
            Vector3 rotateTheFuckingKnob = new Vector3(0, 0, -_xMouse);
            knob.transform.localRotation = Quaternion.Euler(rotateTheFuckingKnob);
        }
    }

    private void OnMouseUp()
    {
        if (isHolding)
        {
            isHolding = false; // Unattatch camera and lamp

            // Return camera back to player and enable player movement
            playerCamera.transform.position = ogCamPos;
            playerCamera.transform.rotation = ogCamRotation;
            playerMovement.enabled = true;
            playerLookAround.enabled = true;
        }
    }

    // Returns the Radio knob rotation on the Z axis
    public float GetKnobRotation()
    {
        return _xMouse;
    }

    public bool isSolved
    {
        get;set;
    }
}
