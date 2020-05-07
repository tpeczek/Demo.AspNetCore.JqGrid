using System;
using System.ComponentModel.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Constants;
using Demo.StartWars.Model;

namespace Demo.AspNetCore.JqGrid.Model
{
    // TODO: cmTemplate
    public class EditableStarWarsCharacterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnEditable]
        public string Name { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.genderFormatter")]
        [JqGridColumnEditable(typeof(DictionariesViewModel), nameof(DictionariesViewModel.GetGendersDictionary), EditType = JqGridColumnEditTypes.Select)]
        public Genders? Gender { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnEditable]
        public int Height { get; set; }

        [Range(0, Int32.MaxValue)]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnEditable]
        public int? Weight { get; set; }

        [Display(Name = "Birth Year")]
        [Required]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnEditable]
        public string BirthYear { get; set; }

        [Display(Name = "Skin Color")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.skinColorFormatter")]
        [JqGridColumnEditable(typeof(DictionariesViewModel), nameof(DictionariesViewModel.GetSkinColorsDictionary), EditType = JqGridColumnEditTypes.Select)]
        public SkinColors? SkinColor { get; set; }

        [Display(Name = "Hair Color")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.hairColorFormatter")]
        [JqGridColumnEditable(typeof(DictionariesViewModel), nameof(DictionariesViewModel.GetHairColorsDictionary), EditType = JqGridColumnEditTypes.Select)]
        public HairColors? HairColor { get; set; }

        [Display(Name = "Eye Color")]
        [Required]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnFormatter("demo.jqGrid.character.eyeColorFormatter")]
        [JqGridColumnEditable(typeof(DictionariesViewModel), nameof(DictionariesViewModel.GetEyeColorsDictionary), EditType = JqGridColumnEditTypes.Select)]
        public EyeColors EyeColor { get; set; }

        [Display(Name = "First Appearance")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter(JqGridPredefinedFormatters.Date, SourceFormat = "ISO8601Long", OutputFormat = "ISO8601Short")]
        [JqGridColumnEditable(EditType = JqGridColumnEditTypes.Date)]
        public DateTime FirstAppearance { get; set; }
    }
}
