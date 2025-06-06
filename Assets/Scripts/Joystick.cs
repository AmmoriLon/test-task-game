using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform background; // Фон джойстика
    [SerializeField] private RectTransform handle;     // Ручка джойстика
    [SerializeField] private float maxRadius = 80f;    // Максимальное смещение ручки
    private Vector2 inputVector;                       // Вектор ввода

    public Vector2 InputVector => inputVector;

    void Start()
    {
        if (background == null || handle == null)
        {
            Debug.LogError("Joystick: Background or Handle not assigned in Inspector!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, null, out pos))
        {
            pos = pos / (background.sizeDelta.x / 2); // Нормализуем позицию
            inputVector = pos.magnitude > 1 ? pos.normalized : pos;
            handle.anchoredPosition = inputVector * maxRadius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}