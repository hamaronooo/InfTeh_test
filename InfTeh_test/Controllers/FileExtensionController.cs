using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.Classes;
using InfTeh_test.DataContexts;
using InfTeh_test.Models.DataContext;
using InfTeh_test.Models.Toast;

namespace InfTeh_test.Controllers
{
    public class FileExtensionController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult _PartialIconCreateForm()
        {
            FileExtension fileExtension = new FileExtension();
            fileExtension.IconsFilesNamesList = new List<string>();
            
            DirectoryInfo d = new DirectoryInfo(HttpContext.Server.MapPath("~/Content/FileIcons/"));
            FileInfo[] Files = d.GetFiles();

            foreach (FileInfo file in Files)
            {
               fileExtension.IconsFilesNamesList.Add(file.Name);
            }

            return PartialView(fileExtension);
        }  
        
        public ActionResult CreateFileExtension(FileExtension fileExtension)
        {
            if (ModelState.IsValid)
            {
                if (db.FileExtensions.Any(m => m.displayname == fileExtension.displayname))
                {
                    var existExt = db.FileExtensions.FirstOrDefault(m => m.displayname == fileExtension.displayname);
                    existExt.icon_filename = fileExtension.icon_filename;
                }
                else
                {
                    fileExtension.displayname = fileExtension.displayname.ToLower();
                    db.FileExtensions.Add(fileExtension);
                }
                db.SaveChanges();
                return RedirectToAction("Partial_CreatedToast", "Toast");
            }
            else
            {
                string allErrors = string.Join("`", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return RedirectToAction("Partial_ToastValidation", "Toast", new { allErrors = allErrors });
            }
        }

        public ActionResult _PartialFileExtensionList()
        {
            List<FileExtension> FileExtList = db.FileExtensions.ToList();
            return  PartialView(FileExtList);
        }

        #region HelpMethods
        private ToastModel ExtensionIsNotUnique()
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
                Message = "Такое расширение уже существует!"
            };
        }
        #endregion
    }
}