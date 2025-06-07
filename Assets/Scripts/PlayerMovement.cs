using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость персонажа
    public Joystick joystick; // Ссылка на джойстик
    private Rigidbody2D rb;
    private PlayerShooting shooting; // Ссылка на скрипт стрельбы

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = GetComponent<PlayerShooting>(); // Находим скрипт стрельбы
        if (joystick == null)
        {
            Debug.LogError("PlayerMovement: Joystick not assigned in Inspector!");
        }
        if (shooting == null)
        {
            Debug.LogError("PlayerMovement: PlayerShooting not found!");
        }
    }

    void Update()
    {
        if (joystick != null)
        {
            Vector2 movement = joystick.InputVector * speed;
            rb.velocity = movement;

            // Поворот персонажа
            if (movement.x != 0)
            {
                float scaleX = Mathf.Sign(movement.x);
                transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }
    }
}