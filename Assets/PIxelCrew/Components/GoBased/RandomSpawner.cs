using PixelCrew.Utils;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class RandomSpawner : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _waitTime;

        [SerializeField] private int _sectorRotation;
        [SerializeField] private int _sectorAngle;

        private Coroutine _routine;


        public void StartDrop(GameObject[] items)
        {
            TryStopRoutine();

            _routine = StartCoroutine(StartSpawn(items));
        }

        private void TryStopRoutine()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
            }  
        }

        private IEnumerator StartSpawn(GameObject[] particles)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Spawn(particles[i]);
                yield return new WaitForSeconds(_waitTime);
            }
        }

        private void Spawn(GameObject particle)
        {
            var instance = SpawnUtils.Spawn(particle, transform.position, transform.rotation);
            var rigitbody = instance.GetComponent<Rigidbody2D>();

            var randomAngle = Random.Range(0, _sectorAngle);
            var forceVector = AngleToVectorInSector(randomAngle);
            rigitbody.AddForce(forceVector * _speed, ForceMode2D.Impulse);
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var position = transform.position;

            var middleAngleDelta = (180 - _sectorRotation - _sectorAngle) / 2;
            var rightBound = GetUnitOnCircle(middleAngleDelta);
            Handles.DrawLine(position, position + rightBound);

            var leftBound = GetUnitOnCircle(middleAngleDelta + _sectorAngle);
            Handles.DrawLine(position, position + leftBound);
            Handles.DrawWireArc(position, Vector3.forward, rightBound, _sectorAngle, 1);

            Handles.color = new Color(1f, 1f, 1f, 0.1f);
            Handles.DrawSolidArc(position, Vector3.forward, rightBound, _sectorAngle, 1);
        }
#endif

        private Vector2 AngleToVectorInSector(float angle)
        {
            var middleAngleDelta = (180 - _sectorRotation - _sectorAngle) / 2;
            return GetUnitOnCircle(middleAngleDelta + angle);
        }

        private Vector3 GetUnitOnCircle(float angleDegrees)
        {
            var angleRadians = Mathf.Deg2Rad * angleDegrees;
            var x = Mathf.Cos(angleRadians);
            var y = Mathf.Sin(angleRadians);
            return new Vector3(x, y, 0);
        }

        private void OnDestroy()
        {
            TryStopRoutine();
        }

        private void OnDisable()
        {
            TryStopRoutine();
        }
    }
}
