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
    public float HeightPosRIG;
    public float MinCamAngle = 30;
    public float MaxCamAngle = 90;

    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        newPosition.y = HeightPosRIG;
        AngleCamChange();
    }

    private void Update()
    {
        HandleMouseInut();
        HandleMovementInput();
    }
    private void FixedUpdate()
    {
    }
    
    private void HandleMouseInut()
    {
        if (Input.mouseScrollDelta.y < 0 && newZoom.y < maxZoom.y && newZoom.z > maxZoom.z)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
            AngleCamChange();
        }
        if (Input.mouseScrollDelta.y > 0 && newZoom.y > minZoom.y && newZoom.z < minZoom.z)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
            AngleCamChange();
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
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))&&newPosition.z < MaxZ)
        {
                newPosition.z += movementSpeed;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))&& newPosition.z > minZ)
        {
                newPosition.z += -movementSpeed;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))&& newPosition.x > minX)
        {
                newPosition.x += -movementSpeed;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))&& newPosition.x < maxX)
        {
                newPosition.x += movementSpeed;
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
    private void AngleCamChange()
    {
        float AngleNeeded = MinCamAngle + ((Vector3.Distance(newZoom, minZoom) / Vector3.Distance(minZoom, maxZoom)) * (MaxCamAngle - MinCamAngle));
        newRotation = Quaternion.Euler(AngleNeeded, 0, 0);
    }
}
