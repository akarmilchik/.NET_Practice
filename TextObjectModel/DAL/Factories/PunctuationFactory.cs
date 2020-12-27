using System.Collections.Generic;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Factories.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    public class PunctuationFactory : ISentenceItemFactory
    {
        private IDictionary<string, ISentenceItem> _cachedItems;

        public ISentenceItem Create(string chars)
        {
            return _cachedItems.ContainsKey(chars) ? _cachedItems[chars] : null;
        }

        public PunctuationFactory()
        {
            _cachedItems = new Dictionary<string, ISentenceItem>();

            foreach (var c in SymbolsContainer.All())
            {
                _cachedItems.Add(c, new Punctuation(c));
            }
        }
    }
}
