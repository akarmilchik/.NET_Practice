using TextObjectModel.App.Interfaces;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    public class SentenceItemFactory : ISentenceItemFactory
    {
        private ISentenceItemFactory punctuationFactory;
        private ISentenceItemFactory wordFactory;
        private IInternService internService;

        public ISentenceItem Create(string chars)
        {
            internService.InternString(chars);

            ISentenceItem result = punctuationFactory.Create(chars);

            if (result == null)
            {
                result = wordFactory.Create(chars);
            }
            return result;
        }

        public SentenceItemFactory(ISentenceItemFactory punctuationFactory, ISentenceItemFactory wordFactory, IInternService internService)
        {
            this.punctuationFactory = punctuationFactory;
            this.wordFactory = wordFactory;
            this.internService = internService;
        }
    }
}
