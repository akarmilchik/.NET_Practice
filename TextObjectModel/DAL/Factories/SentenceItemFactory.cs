using TextObjectModel.App.Interfaces;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    public class SentenceItemFactory : ISentenceItemFactory
    {
        private ISentenceItemFactory _punctuationFactory;
        private ISentenceItemFactory _wordFactory;
        private IInternService _internService;

        public ISentenceItem Create(string chars)
        {
            _internService.InternString(chars);

            ISentenceItem result = _punctuationFactory.Create(chars);

            if (result == null)
            {
                result = _wordFactory.Create(chars);
            }
            return result;
        }

        public SentenceItemFactory(ISentenceItemFactory punctuationFactory, ISentenceItemFactory wordFactory, IInternService internService)
        {
            _punctuationFactory = punctuationFactory;
            _wordFactory = wordFactory;
            _internService = internService;
        }
    }
}
