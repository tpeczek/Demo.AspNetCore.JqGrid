using Demo.StartWars.Model;
using System.ComponentModel.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;

namespace Demo.AspNetCore.JqGrid.Model
{
    public class StarWarsPlanetViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        public string Name { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter("demo.jqGrid.nullAsUnknownFormatter")]
        public int? Diameter { get; set; }

        [Display(Name = "Rotation Period")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter("demo.jqGrid.nullAsUnknownFormatter")]
        public int? RotationPeriod { get; set; }

        [Display(Name = "Orbital Period")]
        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter("demo.jqGrid.nullAsUnknownFormatter")]
        public int? OrbitalPeriod { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter("demo.jqGrid.nullAsUnknownFormatter")]
        public string Gravity { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnFormatter("demo.jqGrid.planet.climateFormatter")]
        public Climates Climate { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnSortable(false)]
        [JqGridColumnFormatter("demo.jqGrid.planet.terrainFormatter")]
        public Terrains Terrain { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center)]
        [JqGridColumnFormatter("demo.jqGrid.nullAsUnknownFormatter")]
        public ulong? Population { get; set; }
    }
}
