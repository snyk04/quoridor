using System.Collections;
using System.Collections.Generic;
using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.View
{
    public abstract class Storage<T> : MonoBehaviour, IEnumerable<T>
    {
        [SerializeField] private List<T> _elements;

        protected abstract int AmountOfColumns { get; }

        private int ToIndex(CellCoordinates elementCoordinates)
        {
            return elementCoordinates.row * AmountOfColumns + elementCoordinates.column;
        }
        public T this[CellCoordinates elementCoordinates]
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
