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
    public class FolderController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult _PartialCreateFolderForm(int? parent_folder)
        {
            return PartialView(parent_folder);
        }
        
        public ActionResult _PartialDeleteFolderForm(int folderid)
        {
            var folder = db.Folders.FirstOrDefault(m => m.folderid == folderid);
            return PartialView(folder);
        }

        public ActionResult _PartialRenameFolderForm(int folderid)
        {
            var folder = db.Folders.FirstOrDefault(m => m.folderid == folderid);
            return PartialView(folder);
        }

        [HttpPost]
        public ActionResult CreateFolder(int? parent_folderid, string name)
        {
          // todo: проверка на уникальность
            if (!string.IsNullOrEmpty(name))
            {
                Folder folder = new Folder();
                folder.displayname = name;
                folder.parent_folderid = parent_folderid;

                if (!IsFolderUnique(folder))
                    return RedirectToAction("Partial_Toast", "Toast", FolderNotUnique());
                
                
                db.Folders.Add(folder);
                db.SaveChanges();
                return RedirectToAction("Partial_CreatedToast","Toast");
            }
            else
            {
                return RedirectToAction("Partial_UnknownErrorToast","Toast");
            }
                
        }

        [HttpPost] 
        public ActionResult DeleteFolder(int folderid) 
        {
            try
            {
                var folder = db.Folders.FirstOrDefault(m => m.folderid == folderid);

                if (folder == null)
                    return RedirectToAction("Partial_Toast", "Toast", NoSuchFolder());

                db.Folders.Remove(folder);
                db.SaveChanges(); 
                
                return RedirectToAction("Partial_DeletedToast", "Toast");
            }
            catch (Exception)
            {
                return RedirectToAction("Partial_UnknownErrorToast", "Toast");
            }
        }

        [HttpPost]
        public ActionResult RenameFolder(int folderid, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                ToastModel validateToast = new ToastModel()
                {
                    IsAutohide = false,
                    BgColor = Colors.danger,
                    BodyTextColor = Colors.white,
                    CloseLinkColor = Colors.dark,
                    CloseText = "Я исправлюсь!",
                    Head = "Ошибка!",
                    HeadTextColor = Colors.white,
                    HeadColor = Colors.danger,
                    Message = "Имя папки не должно быть пустым!"
                };
                return RedirectToAction("Partial_Toast", "Toast", validateToast);
            }

            var folder = db.Folders.FirstOrDefault(m => m.folderid == folderid);

            if (folder == null)
                return RedirectToAction("Partial_Toast", "Toast", NoSuchFolder());

            if (!IsFolderUnique(folder))
                return RedirectToAction("Partial_Toast", "Toast", FolderNotUnique());

            if(newName == folder.displayname)
                return RedirectToAction("Partial_UpdatedToast", "Toast");

            folder.displayname = newName;
            db.SaveChanges();

            return RedirectToAction("Partial_UpdatedToast", "Toast");
        }

        #region HelpMethods
        /// <summary>
        /// Проверка на наличие папки с таким-же именем в бд на одном уровне вложенности
        /// </summary>
        /// <param name="folder"></param>
        /// <returns>false - уникальна, true - уже есть в БД</returns>
        private bool IsFolderUnique(Folder folder)
        {
            return !db.Folders.Where(m => m.parent_folderid == folder.parent_folderid
                && m.folderid != folder.folderid)
                .Any(m=>m.displayname == folder.displayname);
        }

        private ToastModel NoSuchFolder()
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
                Message = "Данная папка не найдена!"
            };
        }

        private ToastModel FolderNotUnique()
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
                Message = "Папка с таким именем уже существует!"
            };
        }
        #endregion
    }
}