using System;
using System.Collections.Generic;
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
                {
                    ToastModel validateToast = new ToastModel()
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
                    return RedirectToAction("Partial_Toast", "Toast", validateToast);
                }
                
                db.Folders.Add(folder);
                db.SaveChanges();
                return RedirectToAction("Partial_CreatedToast","Toast");
            }
            else
            {
                return RedirectToAction("Partial_UnknownErrorToast","Toast");
            }
                
        }

        #region HelpMethods
        /// <summary>
        /// Проверка на наличие папки с таким-же именем в бд на одном уровне вложенности
        /// </summary>
        /// <param name="folder"></param>
        /// <returns>false - уникальна, true - уже есть в БД</returns>
        private bool IsFolderUnique(Folder folder)
        {
            return !db.Folders.Where(m => m.parent_folderid == folder.parent_folderid)
                .Any(m=>m.displayname == folder.displayname);
        }

        #endregion
    }
}