using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    public class Word : IWord
    {
        private Symbol[] _symbols;

        public Word(IEnumerable<Symbol> symbols)
        {
            _symbols = symbols.ToArray();
        }

        public Word(string chars)
        {
            if (chars != null)
            {
                this._symbols = chars.Select(x => new Symbol(x)).ToArray();
            }
            else
            {
                this._symbols = null;
            }
        }

        public Symbol this[int index]
        {
            get { return this._symbols[index]; }
        }

        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var s in this._symbols)
                {
                    sb.Append(s.Chars);
                }
                return sb.ToString();
            }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            //foreach (var s in symbols)
            //{
            //    yield return s;
            //}

            return _symbols.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._symbols.GetEnumerator();
        }


        public int Length
        {
            get { return (_symbols != null) ? _symbols.Length : 0; }
        }

        public ICollection<Symbol> Symbols 
        {
            get { return this._symbols; }
        }
    }
}
