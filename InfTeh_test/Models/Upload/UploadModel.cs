using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace InfTeh_test.Models.Upload
{
    public class UploadModel
    {
        public int? folderid { get; set; }
        public string folderName { get; set; }
        public int Mode { get; set; }
    }
}