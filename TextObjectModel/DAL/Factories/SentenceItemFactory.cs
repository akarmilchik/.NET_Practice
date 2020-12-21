using TextObjectModel.App.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    class SentenceItemFactory : ISentenceItemFactory
    {
        private ISentenceItemFactory punctuationFactory;
        private ISentenceItemFactory wordFactory;

        public ISentenceItem Create(string chars)
        {
            ISentenceItem result = punctuationFactory.Create(chars);
            if (result == null)
            {
                result = wordFactory.Create(chars);
            }
            return result;
        }

        public SentenceItemFactory(ISentenceItemFactory punctuationFactory, ISentenceItemFactory wordFactory)
        {
            this.punctuationFactory = punctuationFactory;
            this.wordFactory = wordFactory;
        }
    }
}
