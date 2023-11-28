using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ProbabilityDropComponent : MonoBehaviour
{
    [SerializeField]private int _count;
    [SerializeField] private DropData[] _drop;
    [SerializeField] private DropEvent _onDropCalculated;

    [SerializeField] private bool _startOnEnable;

    private void OnEnable()
    {
        if (_startOnEnable)
        {
            CalculateDrop();
        }
    }

    [ContextMenu("CalculateDrop")]
    public void CalculateDrop()
    {
        var itemsToDrop = new GameObject[_count];
        var itemCount = 0;
        var total = _drop.Sum(dropData => dropData.Probability);
        var sortedDrop = _drop.OrderBy(dropData => dropData.Probability);

        while (itemCount < _count)
        {
            var random = UnityEngine.Random.value * total;
            foreach (var dropData in sortedDrop)
            {
                if (dropData.Probability >= random)
                {
                    itemsToDrop[itemCount] = dropData.Drop;
                    itemCount++;
                    break;
                }
            }
        }

        _onDropCalculated?.Invoke(itemsToDrop);
    }

    [Serializable]
    private class DropData
    {
        public GameObject Drop;
        [Range(0f, 100f)] public float Probability;
    }

    [Serializable]
    private class DropEvent : UnityEvent<GameObject[]>
    {
    }
}
