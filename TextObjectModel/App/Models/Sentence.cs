using System;
using System.Collections.Generic;
using System.Text;
using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    public class Sentence
    {
        private ICollection<ISentenceItem> items;

        //public Sentence()
        //{
        //    Items = new List<ISentenceItem>();
        //}

        public Sentence(ICollection<ISentenceItem> source)
        {
            items = source;
        }

        public void Add(ISentenceItem item)
        {
            if (item != null)
            {
                items.Add(item);
            }
            else
            {
                throw new NullReferenceException("");
            }
        }

        public bool Remove(ISentenceItem item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<ISentenceItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
