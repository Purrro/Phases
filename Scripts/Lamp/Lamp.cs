using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] GameObject lampBone;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject lights;
    [SerializeField] GameObject key;
    [SerializeField] GameObject lightObject;

    RaycastHit hit;

    private Vector3 ogCamPos;
    private Quaternion ogCamRotation;

    private bool isOn;
    private bool isHolding;

    void Start()
    {
        isOn = false;
        isHolding = false;
    }

    private void OnMouseDown()
    {
        float _dist = Vector3.Distance(playerMovement.transform.position, transform.position); // Check player distance from object

        if (_dist <= 1.5f) // if distance
        {
            isOn = !isOn; // turn light on

            isHolding = true;
            lights.SetActive(isOn);
            playerMovement.enabled = false;

            // store original camera position and rotation
            ogCamPos = playerCamera.transform.position;
            ogCamRotation = playerCamera.transform.rotation;

            // Move Camera to lamp position and rotate lamp to match
            playerCamera.transform.position = lampBone.transform.position;
            lampBone.transform.rotation = playerCamera.transform.rotation;
        }
    }

    private void Update()
    {
        if (isHolding) // if hold item
        {
            lampBone.transform.rotation = playerCamera.transform.rotation; // rotate the lamp as you rotate the camera
        }

        // check if lamp (player) is looking at the key spot
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if (hit.collider.name == "TeenRoomKey" && isHolding && !lightObject.activeInHierarchy)
            {
                key.SetActive(true); // Enable (Show) Key
                hit.collider.enabled = false; // Disable big collider
            }
        }
    }
    private void OnMouseUp()
    {
        isHolding = false; // Unattatch camera and lamp

        // Return camera back to player and enable player movement
        playerCamera.transform.position = ogCamPos;
        playerCamera.transform.rotation = ogCamRotation;
        playerMovement.enabled = true;
    }
}
