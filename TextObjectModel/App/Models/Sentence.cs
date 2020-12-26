using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    public class Sentence : ISentence
    {
        private ICollection<ISentenceItem> _items;

        public int Count => _items.Count();

        public ICollection<ISentenceItem> items
        {
            get { return _items; }
            set { _items = value; }
        }
        public IEnumerator<ISentenceItem> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public Sentence(ICollection<ISentenceItem> source)
        {
            _items = source;
        }

        public void Add(ISentenceItem item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Trying to add a null element to a sentence.");
            }

            _items.Add(item);
        }

        public bool Remove(ISentenceItem item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Trying to remove a null element from a sentence.");
            }

            return _items.Remove(item);
        }
    }
}
