using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Lib.AspNetCore.Mvc.JqGrid.Core.Json;
using Lib.AspNetCore.Mvc.JqGrid.Core.Response;
using Lib.AspNetCore.Mvc.JqGrid.Core.Request;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Searching;
using Demo.StartWars;
using Demo.StartWars.Model;
using Demo.AspNetCore.JqGrid.Model.ModelBinders;

namespace Demo.AspNetCore.JqGrid.Controllers
{
    public class StarWarsController : Controller
    {
        #region Actions
        [AcceptVerbs("GET")]
        public IActionResult CharactersNames(string term)
        {
            return Json(StarWarsContext.Characters.AsQueryable().Where(character => character.Name.StartsWith(term)).Select(character => new { id = character.Id, label = character.Name }));
        }

        [AcceptVerbs("POST")]
        public IActionResult Characters(JqGridRequest request, int? rowId)
        {
            IQueryable<Character> charactersQueryable = (rowId.HasValue) ? StarWarsContext.Characters.AsQueryable().Where(character => character.HomeworldId == rowId.Value) : StarWarsContext.Characters.AsQueryable();
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

        [AcceptVerbs("POST")]
        public IActionResult Planets(JqGridRequest request)
        {
            IQueryable<Planet> planetsQueryable = StarWarsContext.Planets.AsQueryable();

            int totalRecords = planetsQueryable.Count();

            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            planetsQueryable = SortPlanets(planetsQueryable, request.SortingName, request.SortingOrder);

            foreach (Planet planet in planetsQueryable.Skip(request.PageIndex * request.RecordsCount).Take(request.PagesCount * request.RecordsCount))
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(planet.Id), planet));
            }

            response.Reader.RepeatItems = false;

            return new JqGridJsonResult(response);
        }

        [AcceptVerbs("POST")]
        public ActionResult PlanetCharacters(int id)
        {
            JqGridResponse response = new JqGridResponse(true);

            foreach (Character character in StarWarsContext.Characters.Where(c => c.HomeworldId == id))
            {
                response.Records.Add(new JqGridRecord(null, new
                {
                    character.Name,
                    character.BirthYear
                }));
            }

            response.Reader.SubgridReader.RepeatItems = false;

            return new JqGridJsonResult(response);
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
                    filterPredicate = GetCharacterSearchingFilterPredicate(request.SearchingFilter);
                }
                else if (request.SearchingFilters != null)
                {
                    filterPredicate = GetCharacterSearchingFiltersPredicate(request.SearchingFilters);
                }
            }

            return (filterPredicate == null) ? charactersQueryable : charactersQueryable.Where(character => filterPredicate(character));
        }

        private Func<Character, bool> GetCharacterSearchingFiltersPredicate(JqGridSearchingFilters searchingFilters)
        {
            Func<Character, bool> searchingFiltersPredicate = null;
            IList<Func<Character, bool>> searchingFilterPredicates = new List<Func<Character, bool>>();

            foreach (JqGridSearchingFilter searchingFilter in searchingFilters.Filters)
            {
                searchingFilterPredicates.Add(GetCharacterSearchingFilterPredicate(searchingFilter));
            }

            foreach (JqGridSearchingFilters searchingFilterGroup in searchingFilters.Groups)
            {
                searchingFilterPredicates.Add(GetCharacterSearchingFiltersPredicate(searchingFilterGroup));
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

        private Func<Character, bool> GetCharacterSearchingFilterPredicate(JqGridSearchingFilter searchingFilter)
        {
            Func<Character, bool> searchingFilterPredicate = null;

            switch (searchingFilter.SearchingName.ToLowerInvariant())
            {
                case "name":
                    searchingFilterPredicate = GetCharacterNamePredicate(searchingFilter.SearchingOperator, searchingFilter.SearchingValue);
                    break;
                case "gender":
                    searchingFilterPredicate = GetCharacterGenderPredicate(searchingFilter.SearchingOperator, searchingFilter.SearchingValue);
                    break;
            }

            return searchingFilterPredicate;
        }

        private Func<Character, bool> GetCharacterNamePredicate(JqGridSearchOperators searchingOperator, string searchingValue)
        {
            Func<Character, bool> namePredicate = null;

            switch (searchingOperator)
            {
                case JqGridSearchOperators.Eq:
                    namePredicate = (character => String.Compare(character.Name, searchingValue, StringComparison.OrdinalIgnoreCase) == 0);
                    break;
                case JqGridSearchOperators.Ne:
                    namePredicate = (character => String.Compare(character.Name, searchingValue, StringComparison.OrdinalIgnoreCase) != 0);
                    break;
                case JqGridSearchOperators.Bw:
                    namePredicate = (character => character.Name.StartsWith(searchingValue, StringComparison.OrdinalIgnoreCase));
                    break;
                case JqGridSearchOperators.Bn:
                    namePredicate = (character => !character.Name.StartsWith(searchingValue, StringComparison.OrdinalIgnoreCase));
                    break;
                case JqGridSearchOperators.Ew:
                    namePredicate = (character => character.Name.EndsWith(searchingValue, StringComparison.OrdinalIgnoreCase));
                    break;
                case JqGridSearchOperators.En:
                    namePredicate = (character => !character.Name.EndsWith(searchingValue, StringComparison.OrdinalIgnoreCase));
                    break;
                case JqGridSearchOperators.Cn:
                    namePredicate = (character => character.Name.IndexOf(searchingValue, StringComparison.OrdinalIgnoreCase) >= 0);
                    break;
                case JqGridSearchOperators.Nc:
                    namePredicate = (character => character.Name.IndexOf(searchingValue, StringComparison.OrdinalIgnoreCase) == -1);
                    break;
            }

            return namePredicate;
        }

        private Func<Character, bool> GetCharacterGenderPredicate(JqGridSearchOperators searchingOperator, string searchingValue)
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

                    Func<Character, object> sortingExpression = GetCharacterSortingExpression(sortingDetailsName, sortingDetailsOrder);

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

        private Func<Character, object> GetCharacterSortingExpression(string sortingName, JqGridSortingOrders sortingOrder)
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
                            if (character.BirthYear.EndsWith("BBY", StringComparison.OrdinalIgnoreCase))
                            {
                                birthYear = (-1) * Convert.ToDecimal(character.BirthYear.Substring(0, character.BirthYear.Length - 3), CultureInfo.InvariantCulture);
                            }
                            else if (character.BirthYear.EndsWith("ABY", StringComparison.OrdinalIgnoreCase))
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

        private IQueryable<Planet> SortPlanets(IQueryable<Planet> planetsQueryable, string sortingName, JqGridSortingOrders sortingOrder)
        {
            IOrderedEnumerable<Planet> orderedPlanets = null;

            if (!String.IsNullOrWhiteSpace(sortingName))
            {
                sortingName = sortingName.ToLowerInvariant();

                Func<Planet, object> sortingExpression = GetPlanetSortingExpression(sortingName, sortingOrder);

                if (sortingExpression != null)
                {
                    orderedPlanets = (sortingOrder == JqGridSortingOrders.Asc) ? planetsQueryable.OrderBy(sortingExpression) : planetsQueryable.OrderByDescending(sortingExpression);
                }
            }

            return orderedPlanets.AsQueryable();
        }

        private Func<Planet, object> GetPlanetSortingExpression(string sortingName, JqGridSortingOrders sortingOrder)
        {
            Func<Planet, object> sortingExpression = null;

            switch (sortingName)
            {
                case "id":
                    sortingExpression = (planet => planet.Id);
                    break;
                case "name":
                    sortingExpression = (planet => planet.Name);
                    break;
                case "diameter":
                    sortingExpression = (planet => planet.Diameter ?? ((sortingOrder == JqGridSortingOrders.Asc) ? Int32.MaxValue : Int32.MinValue));
                    break;
                case "rotationperiod":
                    sortingExpression = (planet => planet.RotationPeriod ?? ((sortingOrder == JqGridSortingOrders.Asc) ? Int32.MaxValue : Int32.MinValue));
                    break;
                case "orbitalperiod":
                    sortingExpression = (planet => planet.OrbitalPeriod ?? ((sortingOrder == JqGridSortingOrders.Asc) ? Int32.MaxValue : Int32.MinValue));
                    break;
                case "gravity":
                    sortingExpression = (planet => {
                        int gravity = (sortingOrder == JqGridSortingOrders.Asc) ? Int32.MaxValue : Int32.MinValue;

                        if (!String.IsNullOrWhiteSpace(planet.Gravity))
                        {
                            gravity = Convert.ToInt32(planet.Gravity.Replace(" standard", String.Empty), CultureInfo.InvariantCulture);
                        }

                        return gravity;
                    });
                    break;
                case "population":
                    sortingExpression = (planet => planet.Population ?? ((sortingOrder == JqGridSortingOrders.Asc) ? UInt64.MaxValue : UInt64.MinValue));
                    break;
                default:
                    break;
            }

            return sortingExpression;
        }
        #endregion
    }
}
