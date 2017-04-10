using System.ComponentModel.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;

namespace Demo.AspNetCore.JqGrid.Model
{
    public class StarWarsPlanetCharacterViewModel
    {
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 200)]
        public string Name { get; set; }

        [Display(Name = "Birth Year")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 100)]
        public string BirthYear { get; set; }
    }
}
