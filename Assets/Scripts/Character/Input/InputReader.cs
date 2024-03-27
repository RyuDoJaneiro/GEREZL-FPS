using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputReader : MonoBehaviour
    {
        public event Action<Vector2> OnMovementInput = delegate { };
        public event Action OnJumpInput = delegate { };
        public event Action OnShootInput = delegate { };
        public event Action OnReloadInput = delegate { };
        public event Action<float> OnMouseXInput = delegate { };
        public event Action<float> OnMouseYInput = delegate { };

        public void HandleMovementInput(InputAction.CallbackContext context)
        {
            OnMovementInput.Invoke(context.ReadValue<Vector2>());
        }

        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnJumpInput.Invoke();
        }

        public void HandleShootInput(InputAction.CallbackContext context)
        {
            if (context.started)
                OnShootInput.Invoke();
        }

        public void HandleReloadInput(InputAction.CallbackContext context)
        {
            if (context.started)
                OnReloadInput.Invoke();
        }

        public void HandleMouseXInput(InputAction.CallbackContext context)
        {
            OnMouseXInput.Invoke(context.ReadValue<float>());
        }
        
        public void HandleMouseYInput(InputAction.CallbackContext context)
        {
            OnMouseYInput.Invoke(context.ReadValue<float>());
        }
    }
}
