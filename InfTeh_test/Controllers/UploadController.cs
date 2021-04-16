using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.Classes;
using InfTeh_test.DataContexts;
using InfTeh_test.Models.DataContext;
using InfTeh_test.Models.Toast;
using File = InfTeh_test.Models.DataContext.File;

namespace InfTeh_test.Controllers
{
    public class UploadController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult _PartialFiledrop()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files, int? folderid, string description)
        {
            foreach (HttpPostedFileBase file in files)
            {
                if (file != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string fileExt = Path.GetExtension(file.FileName)?.Remove(0, 1);

                    int? extensionid = db.FileExtensions.FirstOrDefault(m => m.displayname == fileExt)
                        ?.file_extensionid;

                    if (extensionid == null)
                    {
                        CreateExtension(fileExt, out int? extid);
                        extensionid = extid;
                    }

                    if (CheckFileExist(fileName, fileExt, folderid))
                    {
                        fileName = fileName + $" ({DateTime.Now.ToString("dd-MM-yy HH:mm:ss")})";
                    }


                    File dbFile = new File();
                    dbFile.folderid = folderid;
                    dbFile.displayname = fileName;
                    dbFile.file_extensionid = extensionid;
                    dbFile.file_content = GetFileBytes(file);
                    dbFile.description = description;

                    db.Files.Add(dbFile);
                    db.SaveChanges();

                    return RedirectToAction("Partial_SuccesUploadedToast", "Toast");

                }
            }
            return RedirectToAction("Partial_UnknownErrorToast", "Toast");
        }

        [HttpPost]
        public ActionResult UploadFileIcon(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string relativePath = "~/Content/FileIcons/" + fileName;
                if (System.IO.File.Exists(HttpContext.Server.MapPath(relativePath)))
                {
                    return RedirectToAction("Partial_FileAlreadyExistsToast", "Toast");
                }
                else
                {
                    file.SaveAs(Server.MapPath("~/Content/FileIcons/" + fileName));
                    return RedirectToAction("Partial_SuccesUploadedToast", "Toast");
                }
            }
            return RedirectToAction("Partial_UnknownErrorToast", "Toast");
        }

        #region HelpMethods

        private bool CheckFileExist(string filename, string ext, int? folderid)
        {
            return db.Files.Any(m=>m.displayname == filename 
                                   && m.FileExtension.displayname == ext
                                   && m.folderid == folderid);
        }

        private void CreateExtension(string name, out int? extid)
        {
            // todo: try catch
            FileExtension fileExtension = new FileExtension()
            {
                displayname = name
            };
            db.FileExtensions.Add(fileExtension);
            db.SaveChanges();
            extid = fileExtension.file_extensionid;
        }

        private byte[] GetFileBytes(HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            return data;
        }
        #endregion
    }
}