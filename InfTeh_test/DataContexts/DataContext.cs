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
        public DbSet<InfTeh_test.Models.DataContext.File> Files { get; set; }
        public DbSet<FileExtension> FileExtensions { get; set; }
    }
}