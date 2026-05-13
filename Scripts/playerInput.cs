using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool IsAttacking { get; private set; }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        IsAttacking = Input.GetMouseButton(0);
    }
}
