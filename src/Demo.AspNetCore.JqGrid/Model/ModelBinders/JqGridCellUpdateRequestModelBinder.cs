using Lib.AspNetCore.Mvc.JqGrid.Core.Request;
using Lib.AspNetCore.Mvc.JqGrid.Core.Request.ModelBinders;
using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.AspNetCore.JqGrid.Model.ModelBinders
{
    internal sealed class CharacterCellUpdateRequestModelBinder : JqGridCellUpdateRequestModelBinder
    {
        #region Fields
        private static readonly IDictionary<string, Type> _supportedCells = new Dictionary<string, Type>
        {
            { "Name", typeof(String) },
            { "Height", typeof(Int32) },
            { "Weight", typeof(Nullable<Int32>) },
            { "BirthYear", typeof(String) }
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
