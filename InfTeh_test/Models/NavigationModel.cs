﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InfTeh_test.Models.DataContext;

namespace InfTeh_test.Models
{
    public class NavigationModel
    {
        public IQueryable<Folder> Folders { get; set; }
        public IQueryable<File> Files { get; set; }

    }
}