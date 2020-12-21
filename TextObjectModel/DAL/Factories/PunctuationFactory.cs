﻿using System.Collections.Generic;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;

namespace TextObjectModel.DAL.Factories
{
    class PunctuationFactory : ISentenceItemFactory
    {
        IDictionary<string, ISentenceItem> cachedItems;

        public ISentenceItem Create(string chars)
        {
            return cachedItems.ContainsKey(chars) ? cachedItems[chars] : null;
        }

        public PunctuationFactory(SeparatorsContainer separatorsContainer)
        {
            this.cachedItems = new Dictionary<string, ISentenceItem>();
            foreach (var c in separatorsContainer.All())
            {
                this.cachedItems.Add(c, new Punctuation(c));
            }
        }
    
    }
}
