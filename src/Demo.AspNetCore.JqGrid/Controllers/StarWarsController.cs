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
        #endregion

        #region Methods
        private IQueryable<Character> FilterCharacters(IQueryable<Character> charactersQueryable, JqGridRequest request)
        {
            if (request.Searching)
            {
                if (request.SearchingFilter != null)
                {
                    charactersQueryable = FilterBySingleCriteria(charactersQueryable, request.SearchingFilter);
                }
            }

            return charactersQueryable;
        }

        private IQueryable<Character> FilterBySingleCriteria(IQueryable<Character> charactersQueryable, JqGridRequestSearchingFilter searchingFilter)
        {
            switch (searchingFilter.SearchingName.ToLowerInvariant())
            {
                case "name":
                    charactersQueryable = FilterByName(charactersQueryable, searchingFilter.SearchingOperator, searchingFilter.SearchingValue);
                    break;
                case "gender":
                    charactersQueryable = FilterByGender(charactersQueryable, searchingFilter.SearchingOperator, searchingFilter.SearchingValue);
                    break;
            }

            return charactersQueryable;
        }

        private IQueryable<Character> FilterByName(IQueryable<Character> charactersQueryable, JqGridSearchOperators searchingOperator, string searchingValue)
        {
            switch (searchingOperator)
            {
                case JqGridSearchOperators.Eq:
                    charactersQueryable = charactersQueryable.Where(character => String.Compare(character.Name, searchingValue, StringComparison.InvariantCultureIgnoreCase) == 0);
                    break;
                case JqGridSearchOperators.Ne:
                    charactersQueryable = charactersQueryable.Where(character => String.Compare(character.Name, searchingValue, StringComparison.InvariantCultureIgnoreCase) != 0);
                    break;
                case JqGridSearchOperators.Bw:
                    charactersQueryable = charactersQueryable.Where(character => character.Name.StartsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.Bn:
                    charactersQueryable = charactersQueryable.Where(character => !character.Name.StartsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.Ew:
                    charactersQueryable = charactersQueryable.Where(character => character.Name.EndsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.En:
                    charactersQueryable = charactersQueryable.Where(character => !character.Name.EndsWith(searchingValue, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case JqGridSearchOperators.Cn:
                    charactersQueryable = charactersQueryable.Where(character => character.Name.IndexOf(searchingValue, StringComparison.InvariantCultureIgnoreCase) >= 0);
                    break;
                case JqGridSearchOperators.Nc:
                    charactersQueryable = charactersQueryable.Where(character => character.Name.IndexOf(searchingValue, StringComparison.InvariantCultureIgnoreCase) == -1);
                    break;
            }

            return charactersQueryable;
        }

        private IQueryable<Character> FilterByGender(IQueryable<Character> charactersQueryable, JqGridSearchOperators searchingOperator, string searchingValue)
        {
            Genders characterGender = Genders.Hermaphrodite;
            if (Enum.TryParse(searchingValue, true, out characterGender))
            {
                switch (searchingOperator)
                {
                    case JqGridSearchOperators.Eq:
                        charactersQueryable = charactersQueryable.Where(character => character.Gender == characterGender);
                        break;
                    case JqGridSearchOperators.Ne:
                        charactersQueryable = charactersQueryable.Where(character => character.Gender != characterGender);
                        break;
                }
            }

            return charactersQueryable;
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
