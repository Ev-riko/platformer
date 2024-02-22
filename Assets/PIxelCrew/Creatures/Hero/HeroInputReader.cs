using PixelCrew.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PixelCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnSaySomething(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.SaySomething();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.Interact();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.Attack();
        }

        public void OnThrow(InputAction.CallbackContext context) 
        {
            if (context.started)
            {
                _hero.StartThrowing();
            }
            if (context.canceled)
            {
                _hero.PerformThrowing();
            }
        }

        public void OnHeal(InputAction.CallbackContext context)
        {
            if (context.started)
                _hero.Throw();
        }

    }
}