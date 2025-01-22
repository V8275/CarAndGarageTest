using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileInput : IInput
{
    private Joystick joystick;
    private bool jumpPressed;

    public MobileInput(Button jumpButton, Joystick joystick)
    {
        this.joystick = joystick;

        // Создаем EventTrigger и добавляем обработчики
        EventTrigger trigger = jumpButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { OnJumpButtonPressed(); });
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { OnJumpButtonReleased(); });
        trigger.triggers.Add(entryUp);
    }

    public Vector2 GetLookInput()
    {
        Vector2 lookInput = Vector2.zero;

        // Проверяем наличие касания
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            // Если касание перемещается, получаем вектор поворота
            if (touch.phase == TouchPhase.Moved)
            {
                lookInput = touch.deltaPosition; // Изменение позиции касания
            }
        }

        return lookInput;
    }

    public Vector2 GetMovementInput()
    {
        return new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    public bool IsJumpPressed()
    {
        return jumpPressed;
    }

    public void HandleInteraction(Transform cameraTransform)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
            else
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
                        if (interactable != null)
                        {
                            interactable.ToggleHold(cameraTransform);
                        }
                    }
                }
            }

        }
    }

    private void OnJumpButtonPressed()
    {
        jumpPressed = true;
    }

    private void OnJumpButtonReleased()
    {
        jumpPressed = false;
    }
}
