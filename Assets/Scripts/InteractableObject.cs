using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset = Vector3.zero;
    private bool isHeld = false;

    // Статическая переменная для отслеживания удерживаемого предмета
    private static InteractableObject currentHeldObject = null;

    public void ToggleHold(Transform newParent)
    {
        if (isHeld)
        {
            // Если уже удерживается, просто отменяем удержание
            ReleaseObject();
        }
        else
        {
            // Если предмет уже удерживается, не даем возможность взять новый
            if (currentHeldObject != null)
            {
                return;
            }

            // Устанавливаем текущий удерживаемый предмет
            currentHeldObject = this;

            isHeld = true;
            // Отключаем физику
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(newParent); // Устанавливаем родителя на камеру
            transform.localPosition = cameraOffset; // Позиционируем перед камерой
        }
    }

    private void ReleaseObject()
    {
        // Освобождаем предмет
        isHeld = false;
        GetComponent<Rigidbody>().isKinematic = false; // Включаем физику
        transform.SetParent(null); // Убираем родительский объект

        // Сбрасываем текущий удерживаемый предмет
        currentHeldObject = null;
    }
}
