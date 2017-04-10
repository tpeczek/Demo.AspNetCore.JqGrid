using System;
using Lib.AspNetCore.Mvc.JqGrid.DataAnnotations;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Enums;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Constants;

namespace Demo.AspNetCore.JqGrid.Model
{
    // TODO: cmTemplate
    public class FileSystemInfoViewModel
    {
        [JqGridColumnLayout(Alignment = JqGridAlignments.Left, Width = 300)]
        public string Name { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 200)]
        [JqGridColumnFormatter(JqGridPredefinedFormatters.Date, SourceFormat = "ISO8601Long", OutputFormat = "d.m.Y H:i:s")]
        public DateTime CreationTime { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 200)]
        [JqGridColumnFormatter(JqGridPredefinedFormatters.Date, SourceFormat = "ISO8601Long", OutputFormat = "d.m.Y H:i:s")]
        public DateTime LastAccessTime { get; set; }

        [JqGridColumnLayout(Alignment = JqGridAlignments.Center, Width = 200)]
        [JqGridColumnFormatter(JqGridPredefinedFormatters.Date, SourceFormat = "ISO8601Long", OutputFormat = "d.m.Y H:i:s")]
        public DateTime LastWriteTime { get; set; }
    }
}
