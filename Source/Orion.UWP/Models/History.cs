using System.Collections.Generic;

namespace Orion.UWP.Models
{
    /// <summary>
    ///     履歴を持つ
    /// </summary>
    public class History<T>
    {
        private readonly List<T> _history;
        private readonly uint _store;

        /// <summary>
        ///     0 is latest value, -1 is previous value.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index] => _history.Count - 1 + index < 0 ? default(T) : _history[_history.Count - 1 + index];

        public History(uint store)
        {
            _history = new List<T>();
            _store = store;
        }

        public void Store(T value)
        {
            if (_history.Count >= _store)
                _history.RemoveAt(0);
            _history.Add(value);
        }
    }
}