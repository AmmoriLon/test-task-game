using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // —сылка на персонажа
    public Vector3 offset;   // —мещение камеры

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
