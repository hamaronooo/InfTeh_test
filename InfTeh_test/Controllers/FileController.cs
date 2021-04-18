using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.Classes;
using InfTeh_test.DataContexts;
using InfTeh_test.Models.File;
using InfTeh_test.Models.Toast;
using File = InfTeh_test.Models.DataContext.File;

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
                if (!db.Files.Any(m=>m.fileid == fileid))
                    return RedirectToAction("Partial_Toast", "Toast", NoSuchFile());

                string fileExt = db.Files.Include(m=>m.FileExtension).FirstOrDefault(m => m.fileid == fileid)?.FileExtension?.displayname;

                if (ImageFileExtensions.Any(fileExt.Contains))
                {
                    var file = db.Files.Include(m => m.FileExtension).FirstOrDefault(m => m.fileid == fileid);
                    return PartialView("_PartialShowFileImage", new FileEditModel(file));
                }

                if (TextFileExtensions.Any(fileExt.Contains))
                {
                    var file = db.Files.Include(m=>m.FileExtension).FirstOrDefault(m => m.fileid == fileid);
                    file.FileContentAsString = file.file_content != null ? Encoding.UTF8.GetString(file.file_content) : "";
                    return PartialView("_PartialShowFileData", new FileEditModel(file));
                }

                // загрузка данных файла без контента файла
                File fileWithoutData = db.Files.AsEnumerable().Where(m => m.fileid == fileid).Select(m => new File
                {
                    fileid = m.fileid,
                    FileExtension = m.FileExtension,
                    description = m.description,
                    displayname = m.displayname,
                    folderid = m.folderid
                }).FirstOrDefault();

                string extName = fileWithoutData.FileExtension?.icon_filename ?? fileWithoutData.FileExtension.displayname + ".svg";
                string relativePath = "/Content/FileIcons/" + extName;
                string fullpath = HttpContext.Server.MapPath(relativePath);

                if (System.IO.File.Exists(fullpath))
                    fileWithoutData.IconFileName = extName;
                else fileWithoutData.IconFileName = "unknown.svg";

                return PartialView("_PartialShowFileExtension", new FileEditModel(fileWithoutData));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Partial_UnknownErrorToast", "Toast");
            }
        }

        [HttpPost]
        public ActionResult SaveFileData(int fileid, string fileName, string description, string content)
        {
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) > 0 || string.IsNullOrWhiteSpace(fileName))
                return RedirectToAction("Partial_Toast", "Toast", InvalidFileName());
            
            try
            {
                var dbFile = db.Files.Include(m=>m.FileExtension)
                    .FirstOrDefault(m => m.fileid == fileid);

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
            "img", "svg", "jpg", "jpeg", "png", "gif", "webp"
        };

        public static string[] TextFileExtensions = new string[]
        {
            "txt", "css", "js", "cs", "cshtml", "html", "php", "xml", "json", "config", "cfg", "sql", "bat"
        };
        private ToastModel InvalidFileName()
        {
            return new ToastModel()
            {
                IsAutohide = false,
                BgColor = Colors.danger,
                BodyTextColor = Colors.white,
                CloseLinkColor = Colors.dark,
                CloseText = "Я сожалею!",
                Head = "Ошибка!",
                HeadTextColor = Colors.white,
                HeadColor = Colors.danger,
                Message = "Имя файла некорректно."
            };
        }
        #endregion
    }
}