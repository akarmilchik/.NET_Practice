using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    class Punctuation : IPunctuation
    {
        private Symbol value;
        public Symbol Value
        {
            get { return this.value; }
        }

        public string Chars
        {
            get { return this.Value.Chars; }
        }

        public Punctuation(string chars)
        {
            this.value = new Symbol(chars);
        }
    }
}
