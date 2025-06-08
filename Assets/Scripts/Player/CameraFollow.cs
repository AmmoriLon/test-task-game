using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ������ �� ������
    public float smoothSpeed = 0.5f; // ����������� �������� �����������
    public Vector3 offset; // �������� ������
    public Vector2 minBounds; // ����������� ������� (����� ������ ����)
    public Vector2 maxBounds; // ������������ ������� (������ ������� ����)
    public Vector2 boundsBuffer = new Vector2(1f, 1f); // ����� ��� ������ ???

    void Start()
    {
        // ��������� ������� �����
        minBounds = new Vector2(-9f, -10f);
        maxBounds = new Vector2(22f, 3f);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // ��������� ������� ������
            Vector3 desiredPosition = target.position + offset;

            // ������������ ������� ������ � �������
            float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + boundsBuffer.x, maxBounds.x - boundsBuffer.x);
            float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + boundsBuffer.y, maxBounds.y - boundsBuffer.y);
            Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

            // ���������� ��������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}