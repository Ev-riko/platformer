using PixelCrew.Components.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthComponent))]
public class MobHealthBarComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _bar;
    private HealthComponent _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        
    }

    void Start()
    {
        
        if (_healthComponent != null)
            _healthComponent.OnChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (_bar != null)
        {
            _bar.size = new Vector2((float)currentHealth / maxHealth, 1);
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log("The Health Bar is not established");
        }
#endif
    }

    private void OnDestroy()
    {
        if (_healthComponent != null)
            _healthComponent.OnChanged -= UpdateHealthBar;
    }
}
