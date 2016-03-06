using Demo.StartWars;
using Demo.StartWars.Model;
using Lib.AspNetCore.Mvc.JqGrid.Core.Response;
using Lib.AspNetCore.Mvc.JqGrid.Core.Json;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using Lib.AspNetCore.Mvc.JqGrid.Core.Request;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;
using System.Globalization;
using System.Collections.Generic;
using Demo.AspNetCore.JqGrid.Model.ModelBinders;

namespace Demo.AspNetCore.JqGrid.Controllers
{
    public class StarWarsController : Controller
    {
        #region Actions
        [AcceptVerbs("POST")]
        public IActionResult Characters(JqGridRequest request)
        {
            IQueryable<Character> charactersQueryable = StarWarsContext.Characters.AsQueryable();
            charactersQueryable = FilterCharacters(charactersQueryable, request);

            int totalRecords = charactersQueryable.Count();

            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords,
                UserData = new
                {
                    Name = "Averages:",
                    Height = StarWarsContext.Characters.Average(character => character.Height),
                    Weight = StarWarsContext.Characters.Average(character => character.Weight)
                }
            };

            charactersQueryable = SortCharacters(charactersQueryable, request.SortingName, request.SortingOrder);

            foreach (Character character in charactersQueryable.Skip(request.PageIndex * request.RecordsCount).Take(request.PagesCount * request.RecordsCount))
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(character.Id), character));
            }

            response.Reader.RepeatItems = false;

            return new JqGridJsonResult(response);
        }

        [AcceptVerbs("POST")]
        public IActionResult UpsertCharacter(Character character)
        {
            bool status = false;

            try
            {
                Character originalCharacter = StarWarsContext.Characters.FirstOrDefault(c => c.Id == character.Id);
                if (originalCharacter == null)
                {
                    character.Id = StarWarsContext.Characters.Max(c => c.Id) + 1;
                    StarWarsContext.Characters.Add(character);
                }
                else
                {
                    originalCharacter.Name = character.Name;
                    originalCharacter.Gender = character.Gender;
                    originalCharacter.Height = character.Height;
                    originalCharacter.Weight = character.Weight;
                    originalCharacter.BirthYear = character.BirthYear;
                    originalCharacter.SkinColor = character.SkinColor;
                    originalCharacter.HairColor = character.HairColor;
                    originalCharacter.EyeColor = character.EyeColor;
                }

                status = true;
            }
            catch
            { }

            return Json(new { Status = status, CharacterId = character.Id });
        }

        [AcceptVerbs("POST")]
        public IActionResult UpdateCharacterProperty([ModelBinder(BinderType = typeof(CharacterCellUpdateRequestModelBinder))]JqGridCellUpdateRequest characterProperty)
        {
            bool status = false;

            try
            {
                int characterId;
                if ((characterProperty != null) && Int32.TryParse(characterProperty.Id, out characterId))
                {
                    Character character = StarWarsContext.Characters.FirstOrDefault(c => c.Id == characterId);
                    if (character != null)
                    {
                        switch (characterProperty.CellName)
                        {
                            case "Name":
                                character.Name = (string)characterProperty.CellValue;
                                break;
                            case "Height":
                                character.Height = (int)characterProperty.CellValue;
                                break;
                            case "Weight":
                                character.Weight = (int?)characterProperty.CellValue;
                                break;
                            case "BirthYear":
                                character.BirthYear = (string)characterProperty.CellValue;
                                break;
                        }

                        status = true;
                    }
                }
            }
            catch
            { }

            return Json(status);
        }

        [AcceptVerbs("POST")]
        public IActionResult DeleteCharacter(int id)
        {
            bool status = false;

            try
            {
                Character character = StarWarsContext.Characters.FirstOrDefault(c => c.Id == id);
                if (character != null)
                {
                    StarWarsContext.Characters.Remove(character);

                    status = true;
                }
            }
            catch
            { }

            return Json(status);
        }
        #endregion

        #region Methods
        private IQueryable<Character> FilterCharacters(IQueryable<Character> charactersQueryable, JqGridRequest request)
        {
            Func<Character, bool> filterPredicate = null;

            if (request.Searching)
            {
                if (request.SearchingFilter != null)
                {
                    filterPredicate = GetSearchingFilterPredicate(request.SearchingFilter);
                }
                else if (request.SearchingFilters != null)
                {
                    filterPredicate = GetSearchingFiltersPredicate(request.SearchingFilters);
                }
            }

            return (filterPredicate == null) ? charactersQueryable : charactersQueryable.Where(character => filterPredicate(character));
        }

        private Func<Character, bool> GetSearchingFiltersPredicate(JqGridRequestSearchingFilters searchingFilters)
        {
            Func<Character, bool> searchingFiltersPredicate = null;
            IList<Func<Character, bool>> searchingFilterPredicates = new List<Func<Character, bool>>();

            foreach (JqGridRequestSearchingFilter searchingFilter in searchingFilters.Filters)
            {
                searchingFilterPredicates.Add(GetSearchingFilterPredicate(searchingFilter));
            }

            foreach (JqGridRequestSearchingFilters searchingFilterGroup in searchingFilters.Groups)
            {
                searchingFilterPredicates.Add(GetSearchingFiltersPredicate(searchingFilterGroup));
            }

            if (searchingFilterPredicates.Any())
            {
                if (searchingFilters.GroupingOperator == JqGridSearchGroupingOperators.And)
                {
                    searchingFiltersPredicate = (character => searchingFilterPredicates.All(searchingFilterPredicate => searchingFilterPredicate(character)));
                }
                else if (searchingFilters.GroupingOperator == JqGridSearchGroupingOperators.Or)
                {
                    searchingFiltersPredicate = (character => searchingFilterPredicates.Any(searchingFilterPredicate => searchingFilterPredicate(character)));
                }
            }

            return searchingFiltersPredicate;
        }

        private Func<Character, bool> GetSearchingFilterPredicate(JqGridRequestSearchingFilter searchingFilter)
        {
            Func<Character, bool> searchingFilterPredicate = null;

            switch (searchingFilter.SearchingName.ToLowerInvariant())
            {
                case "name":
                    searchingFilterPredicate = GetNamePredicate(searchingFilter.SearchingOperator, searchingFilter.SearchingValue);
                    break;
                case "gender":
                    searchingFilterPredicate = GetGenderPredicate(searchingFilter.SearchingOperator, searchingFilter.SearchingValue);
                    break;
            }

            return searchingFilterPredicate;
        }

        private Func<Character, bool> GetNamePredicate(JqGridSearchOperators searchingOperator, string searchingValue)
        {
            Func<Character, bool> namePredicate = null;

            switch (searchingOperator)
            {
                case JqGridSearchOperators.Eq:
                    namePredicate = (character => String.Compare(character.Name, searchingValue, StringComparison.InvariantCultureIgnoreCase) == 0);
                    break;
                case JqGridSearchOperators.Ne:
                    namePredicate = (character => String.Compare(character.Name, searchingValue, StringComparison.InvariantCultureIgnoreCase) != 0);
                    break;
                case JqGridSearchOperators.Bw:
                    namePredicate = (character => character.Name.StartsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.Bn:
                    namePredicate = (character => !character.Name.StartsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.Ew:
                    namePredicate = (character => character.Name.EndsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.En:
                    namePredicate = (character => !character.Name.EndsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.Cn:
                    namePredicate = (character => character.Name.IndexOf(searchingValue, StringComparison.InvariantCultureIgnoreCase) >= 0);
                    break;
                case JqGridSearchOperators.Nc:
                    namePredicate = (character => character.Name.IndexOf(searchingValue, StringComparison.InvariantCultureIgnoreCase) == -1);
                    break;
            }

            return namePredicate;
        }

        private Func<Character, bool> GetGenderPredicate(JqGridSearchOperators searchingOperator, string searchingValue)
        {
            Func<Character, bool> genderPredicate = null;

            Genders characterGender = Genders.Hermaphrodite;
            if (Enum.TryParse(searchingValue, true, out characterGender) || ((searchingOperator & JqGridSearchOperators.NullOperators) != 0))
            {
                switch (searchingOperator)
                {
                    case JqGridSearchOperators.Eq:
                        genderPredicate = (character => character.Gender == characterGender);
                        break;
                    case JqGridSearchOperators.Ne:
                        genderPredicate = (character => character.Gender != characterGender);
                        break;
                    case JqGridSearchOperators.Nu:
                        genderPredicate = (character => !character.Gender.HasValue);
                        break;
                    case JqGridSearchOperators.Nn:
                        genderPredicate = (character => character.Gender.HasValue);
                        break;
                }
            }

            return genderPredicate;
        }

        private IQueryable<Character> SortCharacters(IQueryable<Character> charactersQueryable, string sortingDefition, JqGridSortingOrders sortingOrder)
        {
            IOrderedEnumerable<Character> orderedCharacters = null;

            if (!String.IsNullOrWhiteSpace(sortingDefition))
            {
                sortingDefition = sortingDefition.ToLowerInvariant();

                string[] subSortingDefinitions = sortingDefition.Split(',');
                foreach(string subSortingDefinition in subSortingDefinitions)
                {
                    string[] sortingDetails = subSortingDefinition.Trim().Split(' ');
                    string sortingDetailsName = sortingDetails[0];
                    JqGridSortingOrders sortingDetailsOrder = (sortingDetails.Length > 1) ? (JqGridSortingOrders)Enum.Parse(typeof(JqGridSortingOrders), sortingDetails[1], true) : sortingOrder;

                    Func<Character, object> sortingExpression = GetSortingExpression(sortingDetailsName, sortingDetailsOrder);

                    if (sortingExpression != null)
                    {
                        if (orderedCharacters != null)
                        {
                            orderedCharacters = (sortingDetailsOrder == JqGridSortingOrders.Asc) ? orderedCharacters.ThenBy(sortingExpression) : orderedCharacters.ThenByDescending(sortingExpression);
                        }
                        else
                        {
                            orderedCharacters = (sortingDetailsOrder == JqGridSortingOrders.Asc) ? charactersQueryable.OrderBy(sortingExpression) : charactersQueryable.OrderByDescending(sortingExpression);
                        }
                        
                    }
                }
            }

            return orderedCharacters.AsQueryable();
        }

        private Func<Character, object> GetSortingExpression(string sortingName, JqGridSortingOrders sortingOrder)
        {
            Func<Character, object> sortingExpression = null;

            switch (sortingName)
            {
                case "id":
                    sortingExpression = (character => character.Id);
                    break;
                case "name":
                    sortingExpression = (character => character.Name);
                    break;
                case "gender":
                    sortingExpression = (character => (int?)character.Gender ?? ((sortingOrder == JqGridSortingOrders.Asc) ? Int32.MinValue : Int32.MaxValue));
                    break;
                case "height":
                    sortingExpression = (character => character.Height);
                    break;
                case "weight":
                    sortingExpression = (character => character.Weight ?? ((sortingOrder == JqGridSortingOrders.Asc) ? Int32.MaxValue : Int32.MinValue));
                    break;
                case "birthyear":
                    sortingExpression = (character => {
                        decimal birthYear = (sortingOrder == JqGridSortingOrders.Asc) ? Int32.MaxValue : Int32.MinValue;

                        if (!String.IsNullOrWhiteSpace(character.BirthYear))
                        {
                            if (character.BirthYear.EndsWith("BBY", StringComparison.InvariantCultureIgnoreCase))
                            {
                                birthYear = (-1) * Convert.ToDecimal(character.BirthYear.Substring(0, character.BirthYear.Length - 3), CultureInfo.InvariantCulture);
                            }
                            else if (character.BirthYear.EndsWith("ABY", StringComparison.InvariantCultureIgnoreCase))
                            {
                                birthYear = Convert.ToDecimal(character.BirthYear.Substring(0, character.BirthYear.Length - 3), CultureInfo.InvariantCulture);
                            }
                        }

                        return birthYear;
                    });
                    break;
                default:
                    break;
            }

            return sortingExpression;
        }
        #endregion
    }
}
