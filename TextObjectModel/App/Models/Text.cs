using System.Collections.Generic;
using TextObjectModel.App.Interfaces;

namespace TextObjectModel.App.Models
{
    public class Text
    {
        public ICollection<ISentence> sentences { get; set; }
    }
}
