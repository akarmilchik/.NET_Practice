using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    class Punctuation : IPunctuation
    {
        private Symbol _value;
        public Symbol Value => _value;
        public string Chars
        {
            get { return Value.Chars; }
            set { _value = new Symbol(Chars); }
        }
     

        public Punctuation(string chars)
        {
            _value = new Symbol(chars);
        }
    }
}
