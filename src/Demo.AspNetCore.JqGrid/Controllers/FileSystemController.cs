using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lib.AspNetCore.Mvc.JqGrid.Core.Response;
using Lib.AspNetCore.Mvc.JqGrid.Core.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Demo.AspNetCore.JqGrid.Controllers
{
    public class FileSystemController : Controller
    {
        #region Classes
        private class FileSystemInfoViewModel
        {
            #region Properties
            public string Name { get; set; }
            
            public string CreationTime { get; set; }
            
            public string LastAccessTime { get; set; }
            
            public string LastWriteTime { get; set; }
            #endregion

            #region Constructor
            public FileSystemInfoViewModel(FileSystemInfo info)
            {
                Name = info.Name;
                CreationTime = info.CreationTime.ToString();
                LastAccessTime = info.LastAccessTime.ToString();
                LastWriteTime = info.LastWriteTime.ToString();
            }
            #endregion
        }
        #endregion

        #region Fields
        private readonly IHostingEnvironment _hostingEnvironment;

        private static List<string> _treeGridIdMappings = new List<string>();
        #endregion

        #region Constructor
        public FileSystemController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Actions
        [AcceptVerbs("POST")]
        public IActionResult InfosWeaklyTyped(int? nodeid)
        {
            DirectoryInfo root = GetDirectoryInfo(nodeid);
            IEnumerable<FileSystemInfo> children = GetFileSystemInfos(nodeid);

            if (children != null)
            {
                JqGridResponse response = new JqGridResponse();

                foreach (FileSystemInfo child in children)
                {
                    IList<object> valuesList = new List<object>()
                    {
                        child.Name,
                        child.CreationTime,
                        child.LastAccessTime,
                        child.LastWriteTime,
                    };

                    response.Records.Add(new JqGridAdjacencyTreeRecord(Convert.ToString(GetTreeGridId(child)), valuesList, child.FullName.Count(c => c == '\\') - 2, Convert.ToString(GetTreeGridId(root)))
                    {
                        Leaf = (child is FileInfo)
                    });
                }

                return new JqGridJsonResult(response);
            }
            else
            {
                return new EmptyResult();
            }
        }
        #endregion

        #region Methods
        private int? GetTreeGridId(FileSystemInfo item)
        {
            if (item.FullName == _hostingEnvironment.ContentRootPath)
            {
                return null;
            }
            else if (_treeGridIdMappings.Contains(item.FullName))
            {
                return _treeGridIdMappings.IndexOf(item.FullName);
            }
            else
            {
                _treeGridIdMappings.Add(item.FullName);

                return (_treeGridIdMappings.Count - 1);
            }
        }

        private DirectoryInfo GetDirectoryInfo(int? treeGridId)
        {
            if (treeGridId.HasValue)
            {
                if ((_treeGridIdMappings.Count > treeGridId.Value) && Directory.Exists(_treeGridIdMappings[treeGridId.Value]))
                {
                    return new DirectoryInfo(_treeGridIdMappings[treeGridId.Value]);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return new DirectoryInfo(_hostingEnvironment.ContentRootPath);
            }
        }

        private IEnumerable<FileSystemInfo> GetFileSystemInfos(int? rootTreeGridId)
        {
            DirectoryInfo rootDirectoryInfo = GetDirectoryInfo(rootTreeGridId);

            if (rootDirectoryInfo != null)
            {
                return rootDirectoryInfo.GetFileSystemInfos().OrderByDescending(childFileSystemInfo => childFileSystemInfo is DirectoryInfo);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
