using UnityEngine;

public class PlayerLookAround : MonoBehaviour
{
    //Mouse input properties
    private float _xMouse;
    private float _yMouse;
    [SerializeField] float _mouseSensitivity;

    //Grab properties
    public static bool isGrab;
    float _lerpX, _lerpY, _lerpZ;
    float _grabDistance, _minGrabDistance, _currentGrabDis, _maxGrabDistance;

    //Raycast properties
    Ray ray, rayCheck;
    Transform hitTransform;
    RaycastHit hit;
    Rigidbody hitRB;
    Collider hitC;

    //Camera and other
    [SerializeField] Transform cameraTransform;
    public GameObject crossHair;
    private float _xCamera;

    void Start()
    {
        _maxGrabDistance = 1; //beyong this distance the object will come closer to player (specific to rooms 3 and 4)
        _grabDistance = 1; // this changes depending on how far the player grabbed the object, but he cannot grab it further from 1 in childs room
        _currentGrabDis = 1; // this is to debug after the object is released
        _mouseSensitivity = 250f;
        _minGrabDistance = 0.35f; // objects grabbed too close with default to this distance
    }

    void Update()
    {
        Debug.Log(isGrab);
        RotationAccordingToMouse();
        CheckGrabableViaRay();
        Grab();
        if (!isGrab)
            _grabDistance = _currentGrabDis;
       
    }

    private void CheckGrabableViaRay()
    {
        print(hit.collider);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin += Vector3.forward * 0.01f;

        if (Physics.Raycast(ray, out hit, _grabDistance))
        {
            
            if (hit.collider != null && hit.collider.tag == "Moveable")
            {
                Debug.Log("found object and is moveable");
                if (!isGrab)
                {
                    crossHair.GetComponent<Animator>().Play("Grabable");
                }

                if (Input.GetMouseButtonDown(0)) // the reason this is on mouse button down and not mouse button is: we only want to grab
                {                                // the object the player looked at when clicking. while grabbing an object, said object wont always be directly
                    _grabDistance = hit.distance;// at mouse to world pos, so the ray could hit all sorts of objects while trying to grab a specific one.
                    isGrab = true;
                    hitTransform = hit.transform;

                    if (hitTransform.parent.name == "MoveableP") //if object doesnt have parent
                    {
                        hitTransform.GetComponent<MoveableCont>().isCollided = false;
                    }

                    else
                    {
                        hitTransform.parent.GetComponent<MoveableCont>().isCollided = false;
                    }

                    if (hit.collider.gameObject.GetComponent<BoxCollider>() != null)
                    {
                        hitC = hit.collider.gameObject.GetComponent<BoxCollider>();
                    }

                    else if (hit.collider.gameObject.GetComponent<MeshCollider>() != null)
                    {
                        hitC = hit.collider.gameObject.GetComponent<MeshCollider>();
                    }
                }
            }
                          
            else if (hit.collider != null && hit.collider.tag == "Interactable")
            {
                if (!isGrab)
                {
                    crossHair.GetComponent<Animator>().Play("Grabable");
                }
            }

            else
            {
                crossHair.GetComponent<Animator>().Play("Idle");
            }
            if (Input.GetMouseButtonUp(0))
            {
                crossHair.GetComponent<Animator>().Play("Idle");
                isGrab = false;
            }
        }

        else
        {
            crossHair.GetComponent<Animator>().Play("Idle");
        }
    }

    private void RotationAccordingToMouse()
    {
        _xMouse = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * _xMouse);
        _yMouse = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _xCamera -= _yMouse;
        _xCamera = Mathf.Clamp(_xCamera, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(_xCamera, 0, 0);
    }

    private void Grab()
    {
        if (isGrab)
        {
            crossHair.GetComponent<Animator>().Play("Idle");

            DistanceCheck();

            if (hitTransform != null && hitTransform.parent != null && hitTransform.tag == "Moveable")
            {
                GrabbedObjectMove();

                if (Input.GetMouseButtonUp(0))
                {
                    isGrab = false;
                    _grabDistance = _currentGrabDis;
                }
            }
        }

        else if (!isGrab && hitTransform) // this happens when object is let go
        {
            hitRB.useGravity = true;
            hitRB.constraints = RigidbodyConstraints.None;
        }
    }

    private void GrabbedObjectMove()
    {
        if (hitTransform.parent.name == "MoveableP") //if object doesnt have a parent to set pivot correctly
        {
            //move object gradually to the correct position when grabbed
            hitRB = hitTransform.gameObject.GetComponent<Rigidbody>();
            hitRB.useGravity = false;
            hitRB.constraints = RigidbodyConstraints.FreezeRotation;
            LerpGrabbedObjectToPos();
            hitTransform.Rotate(Vector3.up * _xMouse, Space.World);

            if (hitTransform.GetComponent<MoveableCont>().isCollided) //this checks if the object hit something, and if it did then it will drop
            {
                isGrab = false;
                _grabDistance = _currentGrabDis;
            }
        }

        else // if object has a parent
        {
            //move object parent and said object gradually to the correct position when grabbed
            hitRB = hitTransform.parent.GetComponent<Rigidbody>();
            hitRB.useGravity = false;
            hitRB.constraints = RigidbodyConstraints.FreezeRotation;
            LerpGrabbedObjectsParentToPos();
            hitTransform.transform.parent.transform.Rotate(Vector3.up * _xMouse, Space.World);

            if (hitTransform.parent.GetComponent<MoveableCont>().isCollided)
            {
                isGrab = false;
                _grabDistance = _currentGrabDis;
            }
        }
    }

    private void LerpGrabbedObjectsParentToPos()
    {
        _lerpX = Mathf.Lerp(hitTransform.parent.transform.position.x, (cameraTransform.position + ray.direction * _grabDistance).x, 0.3f);
        _lerpY = Mathf.Lerp(hitTransform.parent.transform.position.y, (cameraTransform.position + ray.direction * _grabDistance).y, 0.3f);
        _lerpZ = Mathf.Lerp(hitTransform.parent.transform.position.z, (cameraTransform.position + ray.direction * _grabDistance).z, 0.3f);
        hitTransform.transform.parent.transform.position = new Vector3(_lerpX, _lerpY, _lerpZ);
    }

    private void LerpGrabbedObjectToPos()
    {
        _lerpX = Mathf.Lerp(hitTransform.position.x, (cameraTransform.position + ray.direction * _grabDistance).x, 0.3f);
        _lerpY = Mathf.Lerp(hitTransform.position.y, (cameraTransform.position + ray.direction * _grabDistance).y, 0.3f);
        _lerpZ = Mathf.Lerp(hitTransform.position.z, (cameraTransform.position + ray.direction * _grabDistance).z, 0.3f);
        hitTransform.position = new Vector3(_lerpX, _lerpY, _lerpZ);
    }

    private void DistanceCheck()
    {
        if (_grabDistance < _minGrabDistance) // if object is grabbed too close to player move it forward a little
        {
            _grabDistance = _minGrabDistance;
        }

        else if (_grabDistance > _maxGrabDistance && isGrab)// opposite 
        {
            _grabDistance = Mathf.Lerp(_grabDistance, _maxGrabDistance, 1);
        }
    }

    public void SetSensetivity(float value)
    {
        _mouseSensitivity = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
       
            //changes grab distance according to the growing player
            if (collision.transform.tag == "TeenFloor")
            {
                _grabDistance = 2;
                _currentGrabDis = 2;
            }

            else if (collision.transform.tag == "AdultFloor" || collision.transform.tag == "ElderFloor")
            {
                _grabDistance = 3;
                _currentGrabDis = 3;
                isGrab = false;

            }
            _minGrabDistance = 0.5f;
       
    }   
}