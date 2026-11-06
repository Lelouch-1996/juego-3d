
using UnityEngine;
using UnityEngine.InputSystem;

public class HellixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private bool isDragging;
    private bool usingTouch;

    public float rotationSpeed = 1f;

    void Update()
    {
        // Detectar inicio del toque en pantalla
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            usingTouch = true;
            isDragging = true;

            lastTapPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // Detectar clic del mouse
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            usingTouch = false;
            isDragging = true;

            lastTapPosition = Mouse.current.position.ReadValue();
        }

        // Procesar movimiento del dedo o mouse
        if (isDragging)
        {
            Vector2 currentTapPosition = lastTapPosition;
            bool pointerPressed = false;
            bool pointerReleased = false;

            if (usingTouch && Touchscreen.current != null)
            {
                var touch = Touchscreen.current.primaryTouch;

                currentTapPosition = touch.position.ReadValue();
                pointerPressed = touch.press.isPressed;
                pointerReleased = touch.press.wasReleasedThisFrame;
            }
            else if (!usingTouch && Mouse.current != null)
            {
                currentTapPosition = Mouse.current.position.ReadValue();
                pointerPressed = Mouse.current.leftButton.isPressed;
                pointerReleased = Mouse.current.leftButton.wasReleasedThisFrame;
            }

            if (pointerPressed)
            {
                float distance =
                    lastTapPosition.x - currentTapPosition.x;

                transform.Rotate(
                    Vector3.up * distance * rotationSpeed,
                    Space.World
                );

                lastTapPosition = currentTapPosition;
            }

            if (pointerReleased)
            {
                isDragging = false;
            }
        }
    }
}
