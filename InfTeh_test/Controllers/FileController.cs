using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.Classes;
using InfTeh_test.DataContexts;
using InfTeh_test.Models.DataContext;
using InfTeh_test.Models.Toast;

namespace InfTeh_test.Controllers
{
    public class FileController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult DownloadFile(int fileid)
        {
            var file = db.Files.FirstOrDefault(m => m.fileid == fileid);
            byte[] filebytes = file.file_content;

            if (filebytes != null)
            {
                var fileExtName = db.FileExtensions.FirstOrDefault(m => m.file_extensionid == file.file_extensionid);
                string filename = file.displayname + "." + fileExtName.displayname;
                return File(filebytes, "application/octet-stream", filename); 
            }
            else return new EmptyResult();
            
        }

        public ActionResult _PartialDeleteFileForm(int fileid)
        {
            File file = db.Files.Where(m => m.fileid == fileid)
                .Include(m=>m.FileExtension).FirstOrDefault();
            return PartialView(file);
        }

        [HttpPost]
        public ActionResult DeleteFile(int fileid)
        {
            try
            {
                var file = db.Files.FirstOrDefault(m => m.fileid == fileid);

                if (file == null)
                    return RedirectToAction("Partial_Toast", "Toast", NoSuchFile());

                db.Files.Remove(file);
                db.SaveChanges();

                return RedirectToAction("Partial_DeletedToast", "Toast");
            }
            catch (Exception)
            {
                return RedirectToAction("Partial_UnknownErrorToast", "Toast");
            }
        }
        
        [HttpPost]
        public ActionResult _PartialShowFileData(int fileid)
        {
            try
            {
                var file = db.Files.FirstOrDefault(m => m.fileid == fileid);

                if (file == null)
                    return RedirectToAction("Partial_Toast", "Toast", NoSuchFile());

                return PartialView(file);
            }
            catch (Exception)
            {
                return RedirectToAction("Partial_UnknownErrorToast", "Toast");
            }
        }

        #region HelpMethods
        private ToastModel NoSuchFile()
        {
            return new ToastModel()
            {
                IsAutohide = false,
                BgColor = Colors.danger,
                BodyTextColor = Colors.white,
                CloseLinkColor = Colors.dark,
                CloseText = "Я исправлюсь!",
                Head = "Ошибка!",
                HeadTextColor = Colors.white,
                HeadColor = Colors.danger,
                Message = "Данный файл не найден!"
            };
        }
        #endregion
    }
}