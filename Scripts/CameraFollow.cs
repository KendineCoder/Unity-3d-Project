using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float xSpeed = 240f;
    public float ySpeed = 240f;

    private float yaw = 0f;
    private float pitch = 0f;

    [Header("Constraints")]
    public float yMinLimit = -20f; // Prevent camera from going too low/underground
    public float yMaxLimit = 80f;  // Prevent camera from flipping over the top
 
    private PlayerInput input;

    void Start()
    {
        input = target.GetComponentInParent<PlayerInput>(); 
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        yaw += input.MouseX * xSpeed * Time.deltaTime;
        pitch -= input.MouseY * ySpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, yMinLimit, yMaxLimit);
        //yaw = Mathf.Clamp(yaw, xMinLimit, xMaxLimit);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
