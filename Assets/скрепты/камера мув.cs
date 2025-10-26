using UnityEngine;

[DisallowMultipleComponent]
public class КамераОтПервогоЛица : MonoBehaviour
{
    [Header("Настройки")]
    public float чувствительность = 200f;
    public float минВертикаль = -80f;
    public float максВертикаль = 80f;
    public Transform игрокРодитель; // если хочешь явно указать родителя, можно задать в инспекторе

    float уголВертикали = 0f;
    CharacterController cc; // ссылка на контроллер игрока (на родителе)

    void Start()
    {
        // если родитель явно не задан — пробуем взять transform.parent
        if (игрокРодитель == null && transform.parent != null)
            игрокРодитель = transform.parent;

        if (игрокРодитель != null)
            cc = игрокРодитель.GetComponent<CharacterController>();

        // если нет CharacterController — всё равно работаем, но сработает проверка в Update
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // проверка: скрипт должен работать ТОЛЬКО если есть CharacterController и он включён
        if (cc == null || !cc.enabled) return;

        float mx = Input.GetAxis("Mouse X") * чувствительность * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * чувствительность * Time.deltaTime;

        // вертикаль: поворачиваем саму камеру (локально)
        уголВертикали -= my;
        уголВертикали = Mathf.Clamp(уголВертикали, минВертикаль, максВертикаль);
        transform.localRotation = Quaternion.Euler(уголВертикали, 0f, 0f);

        // горизонталь: поворачиваем родителя (игрока)
        if (игрокРодитель != null)
            игрокРодитель.Rotate(Vector3.up * mx, Space.Self);
    }
}
