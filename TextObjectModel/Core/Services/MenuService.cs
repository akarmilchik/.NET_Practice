using TextObjectModel.App.Models;
using TextObjectModel.Core.Interfaces;
using TextObjectModel.DAL.Repositories;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Services
{
    class MenuService : IMenuService
    {
        private readonly ITypeConversionService _typeConversionService;
        private readonly IPrintService _printService;
        private readonly IDataRepository _dataRepository;
        private readonly Text _parsedText;

        private int _selectedMenuItemId;

        public MenuService(IDataRepository dataRepository, ITypeConversionService typeConversionService, IPrintService printService, Text parsedText, int selectedMenuItemId)
        {
            this._dataRepository = dataRepository;
            this._typeConversionService = typeConversionService;
            this._printService = printService;
            this._parsedText = parsedText;
            this._selectedMenuItemId = selectedMenuItemId;
        }
        public void CloseApp()
        {
            //_dataRepository.SaveData(_data);
        }


    }
}
