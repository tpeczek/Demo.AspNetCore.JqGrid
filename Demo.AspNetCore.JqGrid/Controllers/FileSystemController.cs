using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Lib.AspNetCore.Mvc.JqGrid.Core.Results;
using Lib.AspNetCore.Mvc.JqGrid.Core.Response;
using Demo.AspNetCore.JqGrid.Model;

namespace Demo.AspNetCore.JqGrid.Controllers
{
    public class FileSystemController : Controller
    {
        #region Fields
        private readonly IWebHostEnvironment _hostingEnvironment;

        private static readonly List<string> _treeGridIdMappings = new List<string>();
        #endregion

        #region Constructor
        public FileSystemController(IWebHostEnvironment hostingEnvironment)
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

        [AcceptVerbs("POST")]
        public IActionResult InfosStronglyTyped(int? nodeid)
        {
            DirectoryInfo root = GetDirectoryInfo(nodeid);
            IEnumerable<FileSystemInfo> children = GetFileSystemInfos(nodeid);

            if (children != null)
            {
                JqGridResponse response = new JqGridResponse();
                response.Reader.RecordId = "Id";
                response.Reader.RepeatItems = false;

                foreach (FileSystemInfo child in children)
                {
                    FileSystemInfoViewModel value = new FileSystemInfoViewModel
                    {
                        Name = child.Name,
                        CreationTime = child.CreationTime,
                        LastAccessTime = child.LastAccessTime,
                        LastWriteTime = child.LastWriteTime
                    };

                    response.Records.Add(new JqGridAdjacencyTreeRecord(Convert.ToString(GetTreeGridId(child)), value, child.FullName.Count(c => c == '\\') - 2, Convert.ToString(GetTreeGridId(root)))
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
