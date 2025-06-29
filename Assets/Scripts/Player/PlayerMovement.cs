using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // �������� ���������
    public Joystick joystick; // ������ �� ��������
    private Rigidbody2D rb;
    private PlayerShooting shooting; // ������ �� ������ ��������
    private PlayerHealth health; // ������ �� ��������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = GetComponent<PlayerShooting>(); // ������� ������ ��������
        health = GetComponent<PlayerHealth>(); // ������� ������ ��������
        if (joystick == null)
        {
            Debug.LogError("PlayerMovement: Joystick not assigned in Inspector!");
        }
        if (shooting == null)
        {
            Debug.LogError("PlayerMovement: PlayerShooting not found!");
        }
        if (health == null)
        {
            Debug.LogError("PlayerMovement: PlayerHealth not found!");
        }
    }

    void Update()
    {
        if (joystick != null)
        {
            Vector2 movement = joystick.InputVector * speed;
            rb.velocity = movement;

            // ������� ���������
            if (movement.x != 0)
            {
                float scaleX = Mathf.Sign(movement.x);
                transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }
    }
}