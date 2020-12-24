using System;

namespace TextObjectModel.App.Models
{
    public struct Symbol
    {
        private string _chars;
        public string Chars
        {
            get { return _chars; }
            private set { _chars = value; }
        }

        public Symbol(string chars)
        {
            _chars = chars;
        }

        public Symbol(char source)
        {
            _chars = String.Format("{0}", source);
        }
    }
}
