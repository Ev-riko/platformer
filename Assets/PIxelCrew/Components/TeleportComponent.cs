using PixelCrew.Creatures;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;
        public void Teleport(Hero hero)
        {
            //target.transform.position = _destTransform.position;
            GameObject target = hero.GameObject();
            StartCoroutine(AnimateTelepot(target));
        }

        private IEnumerator AnimateTelepot(GameObject target)
        {
            var sprite = target.GetComponent<SpriteRenderer>();
            var input = target.GetComponent<PlayerInput>();


            SetLockinput(input, true);
            yield return AlphaAnimation(sprite, 0);
            target.SetActive(false);

            yield return MoveAnimation(target);

            target.SetActive(true);
            yield return AlphaAnimation(sprite, 1);
            SetLockinput(input, false);

        }

        private void SetLockinput(PlayerInput input, bool isLooced)
        {
            if (input != null)
            {
                input.enabled = !isLooced;
            }
        }


        private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
        {
            var alphaTime = 0f;
            var spriteAlpha = sprite.color.a;
            while (alphaTime < _alphaTime)
            {
                alphaTime += Time.deltaTime;
                var progress = alphaTime / _alphaTime;
                var tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);
                var color = sprite.color;
                color.a = tmpAlpha;
                sprite.color = color;

                yield return null;
            }
        }

        private IEnumerator MoveAnimation(GameObject target)
        {
            var moveTime = 0f;
            while (moveTime < _moveTime)
            {
                moveTime += Time.deltaTime;
                var progress = moveTime / _moveTime;
                target.transform.position = Vector3.Lerp(target.transform.position, _destTransform.position, progress);

                yield return null;
            }
        }
    }
}