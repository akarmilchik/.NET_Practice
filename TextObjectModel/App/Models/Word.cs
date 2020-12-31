using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    public class Word : IWord
    {
        private Symbol[] _symbols;

        public Symbol this[int index] => _symbols[index];

        public ICollection<Symbol> Symbols => _symbols;

        public int Length => _symbols?.Length ?? default;

        public IEnumerator<Symbol> GetEnumerator() => _symbols.AsEnumerable()?.GetEnumerator() ?? Enumerable.Empty<Symbol>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();

        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (var s in _symbols)
                {
                    sb.Append(s.Chars);
                }

                return sb.ToString();
            }
            set
            {
                _symbols = Chars.Select(x => new Symbol(x)).ToArray();
            }
        }

        public Word(IEnumerable<Symbol> symbols)
        {
            _symbols = symbols.ToArray();
        }

        public Word(string chars)
        {
            if (chars == null)
            {
                throw new NullReferenceException("Trying to add a null element to the word.");
            }

            _symbols = chars.Select(x => new Symbol(x)).ToArray();
        }
    }
}
