using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InfTeh_test.Models.File
{
    [NotMapped]
    public class FileEditModel : DataContext.File
    {
        public FileEditModel(DataContext.File file)
        {
            fileid = file.fileid;
            displayname = file.displayname;
            description = file.description;
            file_extensionid = file.file_extensionid;
            folderid = file.folderid;
            file_content = file.file_content;
            FileExtension = file.FileExtension;
            Folder = file.Folder;
            FileContentAsString = file.FileContentAsString;
        }
    }
}