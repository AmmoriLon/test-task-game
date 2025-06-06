using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость персонажа
    public Joystick joystick; // Ссылка на джойстик
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Получаем ввод от джойстика
        Vector2 movement = joystick.InputVector * speed;
        rb.velocity = movement;
    }
}