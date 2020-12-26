using TextObjectModel.App.Interfaces;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    public class SentenceItemFactory : ISentenceItemFactory
    {
        private readonly ISentenceItemFactory _punctuationFactory;
        private readonly ISentenceItemFactory _wordFactory;
        private readonly IInternService _internService;

        public SentenceItemFactory(ISentenceItemFactory punctuationFactory, ISentenceItemFactory wordFactory, IInternService internService)
        {
            _punctuationFactory = punctuationFactory;
            _wordFactory = wordFactory;
            _internService = internService;
        }

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
    }
}
