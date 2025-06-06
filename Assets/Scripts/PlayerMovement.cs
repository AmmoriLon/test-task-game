using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // �������� ���������
    public Joystick joystick; // ������ �� ��������
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �������� ���� �� ���������
        Vector2 movement = joystick.InputVector * speed;
        rb.velocity = movement;
    }
}