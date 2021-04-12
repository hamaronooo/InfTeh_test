using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using InfTeh_test.Models.DataContext;

namespace InfTeh_test.DataContexts
{
    public class DataContext : DbContext
    {
        public DataContext() : base("database") { }

        public DbSet<Folder> Folders { get; set; }
    }
}