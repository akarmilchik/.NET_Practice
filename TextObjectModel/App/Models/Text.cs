using System;
using System.Collections.Generic;
using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    public class Text
    {
        private ICollection<ISentence> _sentences;

        public ICollection<ISentence> sentences 
        { 
            get { return _sentences; } 
            set { _sentences = value; } 
        }
        
        public Text(ICollection<ISentence> sentences)
        {
            _sentences = sentences;
        }

        public void Add(ISentence item)
        {
            if (item != null)
            {
                sentences.Add(item);
            }
            else
            {
                throw new NullReferenceException("");
            }
        }

    }
}
