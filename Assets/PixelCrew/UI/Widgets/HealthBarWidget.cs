using PixelCrew.Components.Health;
using PixelCrew.Utils.Disposebles;
using UnityEngine;

namespace PixelCrew.UI.Widgets
{
    public class HealthBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _progressBar;
        [SerializeField] private HealthComponent _hp;

        private readonly CompositeDisposeble _trash = new CompositeDisposeble();

        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();


            _trash.Retain(_hp._onDie.Subscribe(OnDie));
            _trash.Retain(_hp._onChanged.Subscribe(OnHpChanged));
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float)hp / _hp.MaxHealth;
            _progressBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}