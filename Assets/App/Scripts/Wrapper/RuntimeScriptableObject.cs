using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BT.ScriptablesObject
{
    public class RuntimeScriptableObject<T> : ScriptableObject
    {
        [ReadOnly][ShowInInspector] private T _value = default(T);
        
        public T Value
        {
            get => _value;
            [Button]
            set
            {
                _value = value;
                OnChanged?.Invoke();
            }
        }

        public event Action OnChanged;
    }
}
