using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.Classes;
using InfTeh_test.DataContexts;
using InfTeh_test.Models.DataContext;
using InfTeh_test.Models.File;
using InfTeh_test.Models.Toast;

namespace InfTeh_test.Controllers
{
    public class FileController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult DownloadFile(int fileid)
        {
            var file = db.Files.FirstOrDefault(m => m.fileid == fileid);
            
            if (file.file_content == null)
                file.file_content = Encoding.ASCII.GetBytes("");


            var fileExtName = db.FileExtensions.FirstOrDefault(m => m.file_extensionid == file.file_extensionid);
            string filename = file.displayname + "." + fileExtName?.displayname;
            return File(file.file_content, "application/octet-stream", filename);

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
        public ActionResult ShowFileData(int fileid)
        {
            try
            {
                var file = db.Files.Include(m=>m.FileExtension).FirstOrDefault(m => m.fileid == fileid);

                if (file == null)
                    return RedirectToAction("Partial_Toast", "Toast", NoSuchFile());

                file.FileContentAsString = file.file_content != null ? Encoding.UTF8.GetString(file.file_content) : "";

                FileEditModel fileEditModel = new FileEditModel(file);

                if (ImageFileExtensions.Any(file.FileExtension.displayname.Contains))
                    return PartialView("_PartialShowFileImage", fileEditModel);
                if (TextFileExtensions.Any(file.FileExtension.displayname.Contains))
                    return PartialView("_PartialShowFileData", fileEditModel);
               
                
                return PartialView("_PartialShowFileExtension", fileEditModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Partial_UnknownErrorToast", "Toast");
            }
        }

        [HttpPost]
        public ActionResult SaveFileData(int fileid, string fileName, string description, string content)
        {
            try
            {
                var dbFile = db.Files.Include(m=>m.FileExtension)
                    .FirstOrDefault(m => m.fileid == fileid);

                if (dbFile != null && TextFileExtensions.Any(dbFile.FileExtension.displayname.Contains))
                {
                    dbFile.file_content = Encoding.ASCII.GetBytes(content);
                }

                dbFile.displayname = fileName;
                dbFile.description = description;

                //db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Partial_UpdatedToast", "Toast");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Partial_UnknownErrorToast", "Toast");
            }
        }
//        try
//        {
//            if (!ModelState.IsValid)
//            {
//                string allErrors = string.Join("`", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
//                return RedirectToAction("Partial_ToastValidation", "Toast", new { allErrors = allErrors
//                });
//            }

//            var dbFile = db.Files.Include(m => m.FileExtension)
//                .FirstOrDefault(m => m.fileid == fileEditModel.fileid);

//            if (dbFile != null && TextFileExtensions.Any(dbFile.FileExtension.displayname.Contains))
//            {
//                dbFile.file_content = Encoding.ASCII.GetBytes(fileEditModel.FileContentAsString);
//            }

//            dbFile.displayname = fileEditModel.displayname;
//            dbFile.description = fileEditModel.description;

////db.Entry(file).State = EntityState.Modified;
//            db.SaveChanges();

//            return RedirectToAction("Partial_UpdatedToast", "Toast");
//        }
//        catch (Exception ex)
//        {
//            //foreach (var entityValidationErrors in ex.EntityValidationErrors)
//            //{
//            //    foreach (var validationError in entityValidationErrors.ValidationErrors)
//            //    {
//            //        erre = "Property: " + validationError.PropertyName + " Error: " +
//            //                       validationError.ErrorMessage;
//            //    }
//            //}
//            return RedirectToAction("Partial_UnknownErrorToast", "Toast");
//        }
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
                Message = "Данный файл не найден, возможно он уже удален!"
            };
        }

        public static string[] ImageFileExtensions = new string[]
        {
            "img", "svg", "jpg", "jpeg", "png", "gif"
        };

        public static string[] TextFileExtensions = new string[]
        {
            "txt", "css", "js"
        };

        #endregion
    }
}