using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float rotationAmount = 3f;    // насколько сильно отставать
    public float smooth = 6f;             // плавность
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Создаём небольшое смещение в зависимости от движения мыши
        Quaternion xAdj = Quaternion.AngleAxis(-rotationAmount * mouseY, Vector3.right);
        Quaternion yAdj = Quaternion.AngleAxis(rotationAmount * mouseX, Vector3.up);
        Quaternion targetRotation = initialRotation * yAdj * xAdj;

        // Плавно возвращаем к нормали
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
