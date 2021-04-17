using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InfTeh_test.Models.DataContext;

namespace InfTeh_test.Models
{
    public class NavigationModel
    {
        public Folder ParentFolder { get; set; }
        public IQueryable<Folder> Folders { get; set; }
        public List<DataContext.File> Files { get; set; }
    }
}