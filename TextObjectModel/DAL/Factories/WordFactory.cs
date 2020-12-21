﻿using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;

namespace TextObjectModel.DAL.Factories
{
    class WordFactory : ISentenceItemFactory
    {
        public ISentenceItem Create(string chars)
        {
            return new Word(chars);
        }
    }
}
