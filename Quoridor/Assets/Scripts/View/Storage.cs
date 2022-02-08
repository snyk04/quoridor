using System.Collections;
using System.Collections.Generic;
using Quoridor.Model.Common;
using UnityEngine;

namespace Quoridor.View
{
    public abstract class Storage<T> : MonoBehaviour, IEnumerable<T>
    {
        [SerializeField] private List<T> _elements;

        protected abstract int AmountOfColumns { get; }

        private int ToIndex(Coordinates elementCoordinates)
        {
            return elementCoordinates.Row * AmountOfColumns + elementCoordinates.Column;
        }
        public T this[Coordinates elementCoordinates]
        {
            get
            {
                int wallIndex = ToIndex(elementCoordinates);
                return _elements[wallIndex];
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
