﻿using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Factories.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    public class WordFactory : ISentenceItemFactory
    {
        public ISentenceItem Create(string chars) => new Word(chars);
    }
}
