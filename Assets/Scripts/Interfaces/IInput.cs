using UnityEngine;

public interface IInput
{
    Vector2 GetMovementInput();
    bool IsJumpPressed();
    Vector2 GetLookInput();
    void HandleInteraction(Transform cameraTransform);
}
