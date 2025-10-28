using UnityEngine;

public class WeaponWallAvoid : MonoBehaviour
{
    public Transform cam;                 // Камера игрока
    public float checkDistance = 1f;      // На каком расстоянии проверять стену
    public float moveSpeed = 8f;          // Скорость смещения
    public Vector3 normalOffset = new Vector3(0f, -0.05f, 0.3f);   // нормальное положение
    public Vector3 closeOffset = new Vector3(0f, -0.05f, 0.1f);    // ближе к камере при стене

    private Vector3 targetOffset;

    void Start()
    {
        targetOffset = normalOffset;
    }

    void Update()
    {
        // Линия вперёд из камеры
        bool closeToWall = Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, checkDistance);

        // Меняем целевое смещение в зависимости от того, близко ли стена
        targetOffset = closeToWall ? closeOffset : normalOffset;

        // Плавно двигаем оружие
        Vector3 desiredPosition = cam.TransformPoint(targetOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * moveSpeed);

        // Поворот под камеру
        transform.rotation = Quaternion.Lerp(transform.rotation, cam.rotation, Time.deltaTime * moveSpeed);
    }
}
