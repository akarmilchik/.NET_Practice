using NewYearGift.App.Constants;
using NewYearGift.App.Interfaces;
using NewYearGift.App.Models;
using NewYearGift.App.Models.Sweets;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IDataRepository _jsonDataRepository;
        private readonly ITypeConversionService _typeConversionService;
        private readonly IPrintService _printService;
        private readonly IGiftService _giftService;
        private readonly JsonDataModel _data;
        private int _selectedMenuItemId;

        public MainMenuService(IDataRepository jsonDataRepository, ITypeConversionService typeConversionService, IPrintService printService, IGiftService giftService, JsonDataModel data, int selectedMenuItemId)
        {
            this._jsonDataRepository = jsonDataRepository;
            this._typeConversionService = typeConversionService;
            this._printService = printService;
            this._giftService = giftService;
            this._data = data;
            this._selectedMenuItemId = selectedMenuItemId;
        }

        public void CloseApp()
        {
            _jsonDataRepository.SaveData(_data);
        }

        public void MakeNewGift()
        {
            string sweetsRange;

            IEnumerable<Sweet> sweets;

            _printService.PrintChoosePresenteeMenu();

            var inputParams = Console.ReadKey();

            _selectedMenuItemId = _typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

            _printService.PrintSweetsMenu();

            sweets = _giftService.GetSweetsByPresentee(_data.AllSweets, (Presentee)_selectedMenuItemId);

            _printService.PrintSweets(sweets.ToList());

            _printService.PrintInputText();

            sweetsRange = Console.ReadLine();

            var range = sweetsRange.Split(' ');

            var intRange = _typeConversionService.CheckAndConvertInputArrayToInt(range);

            var resultSweets = _giftService.GetSweetsByIndexRange(sweets, intRange);

            _data.Gift = _giftService.MakeGift(resultSweets.ToList(), (Presentee)_selectedMenuItemId);

            _printService.PrintGift(_data.Gift);
        }

        public void SortGiftSweetsByParameter()
        {
            IEnumerable<Sweet> sweets;

            SortMenuItems sortMenuItems;

            _printService.PrintSweetParametersMenu();

            var inputParams = Console.ReadKey();

            _printService.PrintSortingMenu();

            var inputSorting = Console.ReadKey();

            var inputSortingInt = _typeConversionService.CheckAndConvertInputToInt(inputSorting.KeyChar.ToString());

            var sortOrder = (SortOrder)inputSortingInt;

            _selectedMenuItemId = _typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

            sortMenuItems = (SortMenuItems)_selectedMenuItemId;

            switch (sortMenuItems)
            {
                case SortMenuItems.SortByName:
                    sweets = _giftService.SortSweetBy(s => s.Name, _data.Gift.Sweets, sortOrder);
                    _printService.PrintSweets(sweets.ToList());
                    break;
                case SortMenuItems.SortByWeight:
                    sweets = _giftService.SortSweetBy(s => s.Weight, _data.Gift.Sweets, sortOrder);
                    _printService.PrintSweets(sweets.ToList());
                    break;
                case SortMenuItems.SortByCalorie:
                    sweets = _giftService.SortSweetBy(s => s.Kkal, _data.Gift.Sweets, sortOrder);
                    _printService.PrintSweets(sweets.ToList());
                    break;
                case SortMenuItems.SortByFillingName:
                    sweets = _giftService.SortSweetBy(s => s.Filling.Name, _data.Gift.Sweets, sortOrder);
                    _printService.PrintSweets(sweets.ToList());
                    break;
                case SortMenuItems.SortByShapeName:
                    sweets = _giftService.SortSweetBy(s => s.Shape.Name, _data.Gift.Sweets, sortOrder);
                    _printService.PrintSweets(sweets.ToList());
                    break;
                default:
                    _printService.PrintWrongInput();
                    break;
            }
        }

        public void FindGiftSweetsByParameter()
        {
            IEnumerable<Sweet> findSweets;

            _printService.PrintSweetRangeParametersMenu();

            var inputParams = Console.ReadKey();

            _selectedMenuItemId = _typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

            _printService.PrintStartRangeText();

            var inputRangeValue = Console.ReadLine();

            var firstRangeValue = _typeConversionService.CheckAndConvertInputToInt(inputRangeValue);

            _printService.PrintEndRangeText();

            inputRangeValue = Console.ReadLine();

            var lastRangeValue = _typeConversionService.CheckAndConvertInputToInt(inputRangeValue);
            
            switch (_selectedMenuItemId)
            {
                case 1:
                    findSweets = _giftService.GetSweetsByRange(s => s.Weight ,_data.Gift.Sweets, firstRangeValue, lastRangeValue);
                    _printService.PrintSweets(findSweets.ToList());
                    break;
                case 2:
                    findSweets = _giftService.GetSweetsByRange(s => s.Kkal, _data.Gift.Sweets, firstRangeValue, lastRangeValue);
                    _printService.PrintSweets(findSweets.ToList());
                    break;
            }
        }
    }
}
