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

        EventTrigger trigger = jumpButton.gameObject.AddComponent<EventTrigger>();

        // check click button
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { OnJumpButtonPressed(); });
        trigger.triggers.Add(entryDown);

        // check up button
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { OnJumpButtonReleased(); });
        trigger.triggers.Add(entryUp);
    }

    public Vector2 GetLookInput()
    {
        Vector2 lookInput = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            if (touch.phase == TouchPhase.Moved)
            {
                lookInput = touch.deltaPosition;
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

    //to interact with objects
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
