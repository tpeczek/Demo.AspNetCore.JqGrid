using System;
using Demo.StartWars.Model;
using System.ComponentModel.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Constants;

namespace Demo.AspNetCore.JqGrid.Model
{
    // TODO: cmTemplate
    public class SearchableStarWarsCharacterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSearchable("CharactersNames", "StarWars", SearchType = JqGridColumnSearchTypes.JQueryUIAutocomplete, SearchOperators = JqGridSearchOperators.TextOperators ^ JqGridSearchOperators.NullOperators)]
        public string Name { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter("demo.jqGrid.character.genderFormatter")]
        [JqGridColumnSearchable(typeof(DictionariesViewModel), nameof(DictionariesViewModel.GetGendersDictionary), SearchType = JqGridColumnSearchTypes.Select, SearchOperators = JqGridSearchOperators.EqualOrNotEqual | JqGridSearchOperators.NullOperators)]
        public Genders? Gender { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSummary(JqGridColumnSummaryTypes.Avg)]
        [JqGridColumnSearchable(false)]
        public int Height { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSummary(JqGridColumnSummaryTypes.Avg)]
        [JqGridColumnSearchable(false)]
        public int? Weight { get; set; }

        [Display(Name = "Birth Year")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSearchable(false)]
        public string BirthYear { get; set; }

        [Display(Name = "Skin Color")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnSearchable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.skinColorFormatter")]
        public SkinColors? SkinColor { get; set; }

        [Display(Name = "Hair Color")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnSearchable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.hairColorFormatter")]
        public HairColors? HairColor { get; set; }

        [Display(Name = "Eye Color")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnSearchable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.eyeColorFormatter")]
        public EyeColors EyeColor { get; set; }

        [Display(Name = "First Appearance")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter(JqGridPredefinedFormatters.Date, SourceFormat = "ISO8601Long", OutputFormat = "ISO8601Short")]
        [JqGridColumnSearchable(SearchType = JqGridColumnSearchTypes.Date, SearchOperators = JqGridSearchOperators.Eq | JqGridSearchOperators.Ge | JqGridSearchOperators.Gt | JqGridSearchOperators.Le | JqGridSearchOperators.Lt)]
        public DateTime FirstAppearance { get; set; }
    }
}
