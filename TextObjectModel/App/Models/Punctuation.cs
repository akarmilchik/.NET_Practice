using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    class Punctuation : IPunctuation
    {
        private Symbol _value;
        public Symbol Value => _value;
        public string Chars => Value.Chars;

        public Punctuation(string chars)
        {
            this._value = new Symbol(chars);
        }
    }
}
