using UnityEngine;

public class CamerController : MonoBehaviour
{
    public Transform cameraTransform;

    [Header("Current Value")]
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;
    [Header("Rules For Camera")]
    public Vector3 zoomAmount;
    public Vector3 maxZoom;
    public Vector3 minZoom;
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public float minX, maxX;
    public float minZ, MaxZ;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInut();
        HandleMovementInput();
    }

    private void HandleMouseInut()
    {

        if (Input.mouseScrollDelta.y < 0 && newZoom.y < maxZoom.y && newZoom.z > maxZoom.z)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;


            newRotation.x = -0.25f + (Vector3.Distance(newZoom, minZoom) / Vector3.Distance(minZoom, maxZoom)) * 0.40f;

            print(newRotation.x);
        }
        if (Input.mouseScrollDelta.y > 0 && newZoom.y > minZoom.y && newZoom.z < minZoom.z)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;

            newRotation.x = -0.25f + (Vector3.Distance(newZoom, minZoom)/Vector3.Distance(minZoom, maxZoom))*0.40f;
            print(newRotation.x);
        }
    }


    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if(newPosition.z < MaxZ)
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if(newPosition.z >minZ)
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(newPosition.x > minX)
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(newPosition.x < maxX)
            newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.R) && newZoom.y > minZoom.y && newZoom.z < minZoom.z)
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F) && newZoom.y < maxZoom.y && newZoom.z > maxZoom.z)
        {
            newZoom -= zoomAmount;
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
}
