using Lib.AspNetCore.Mvc.JqGrid.Core.Request.ModelBinders;
using System;
using System.Collections.Generic;
using Demo.StartWars.Model;

namespace Demo.AspNetCore.JqGrid.Model.ModelBinders
{
    internal sealed class CharacterCellUpdateRequestModelBinder : JqGridCellUpdateRequestModelBinder
    {
        #region Fields
        private static readonly IDictionary<string, Type> _supportedCells = new Dictionary<string, Type>
        {
            { nameof(Character.Name), typeof(String) },
            { nameof(Character.Height), typeof(Int32) },
            { nameof(Character.Weight), typeof(Nullable<Int32>) },
            { nameof(Character.BirthYear), typeof(String) },
            { nameof(Character.FirstAppearance), typeof(DateTime) }
        };
        #endregion

        #region Properties
        protected override IDictionary<string, Type> SupportedCells
        {
            get { return _supportedCells; }
        }
        #endregion
    }
}
