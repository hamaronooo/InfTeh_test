using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using InfTeh_test.DataContexts;
using InfTeh_test.Models.DataContext;

namespace InfTeh_test.Classes
{
    public class FilesWorker
    {
        public List<Models.DataContext.File> GetFilesByFolderID(int? folderid)
        {
            List<File> result = new List<File>();
            using (DataContext db = new DataContext())
            {
                result = db.Files
                    .Where(m => m.folderid == folderid)
                    .Include(m => m.FileExtension)
                    .Select(m => new
                    {
                        fileid = m.fileid,
                        displayname = m.displayname,
                        description = m.description,
                        file_extensionid = m.file_extensionid,
                        folderid = m.folderid,
                        FileExtension = m.FileExtension
                    }).ToList().Select(x => new File()
                    {
                        fileid = x.fileid,
                        displayname = x.displayname,
                        description = x.description,
                        file_extensionid = x.file_extensionid,
                        folderid = x.folderid,
                        FileExtension = x.FileExtension
                    }).ToList();
            }

            foreach (File file in result)
            {
                string extName = file.FileExtension?.icon_filename ?? file.FileExtension.displayname + ".svg";
                string relativePath = "/Content/FileIcons/" + extName;
                string fullpath = HttpContext.Current.Server.MapPath(relativePath);

                bool a = System.IO.File.Exists(fullpath);

                if (System.IO.File.Exists(fullpath))
                    file.IconFileName = extName;
                else file.IconFileName = "unknown.svg";
            }

            return result;
        }

        public List<Models.DataContext.File> GetFilesByDisplaynameOrExt(string searchInput)
        {
            List<File> result = new List<File>();
            using (DataContext db = new DataContext())
            {
                result = db.Files
                    .Where(m => m.displayname.Contains(searchInput)
                                || m.FileExtension.displayname.Contains(searchInput))
                    .Include(m => m.FileExtension)
                    .Select(m => new
                    {
                        fileid = m.fileid,
                        displayname = m.displayname,
                        description = m.description,
                        file_extensionid = m.file_extensionid,
                        folderid = m.folderid,
                        FileExtension = m.FileExtension
                    }).ToList().Select(x => new File()
                    {
                        fileid = x.fileid,
                        displayname = x.displayname,
                        description = x.description,
                        file_extensionid = x.file_extensionid,
                        folderid = x.folderid,
                        FileExtension = x.FileExtension
                    }).ToList();
            }

            foreach (File file in result)
            {
                string extName = file.FileExtension?.icon_filename ?? file.FileExtension.displayname + ".svg";
                string relativePath = "/Content/FileIcons/" + extName;
                string fullpath = HttpContext.Current.Server.MapPath(relativePath);

                bool a = System.IO.File.Exists(fullpath);

                if (System.IO.File.Exists(fullpath))
                    file.IconFileName = extName;
                else file.IconFileName = "unknown.svg";
            }

            return result;
        }
    }
}