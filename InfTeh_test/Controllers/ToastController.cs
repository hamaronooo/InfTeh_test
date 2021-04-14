using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.Classes;
using InfTeh_test.Models.Toast;

namespace InfTeh_test.Controllers
{
    public class ToastController : Controller
    {
        public ActionResult Partial_ToastContainer()
        {
            return PartialView();
        }

        public ActionResult Partial_Toast(ToastModel toast)
        {
            return PartialView(toast);
        }

        public ActionResult Partial_CreatedToast()
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Успех";
            toast.CloseText = "Закрыть";
            toast.Message = "Успешно добавлено!";
            toast.Delay_ms = 20000;
            toast.BgColor = Colors.white;
            toast.HeadColor = Colors.success;
            toast.HeadTextColor = Colors.white;
            toast.BodyTextColor = Colors.dark;
            toast.CloseLinkColor = Colors.white;
            return PartialView("Partial_Toast", toast);
        }
        
        public ActionResult Partial_SuccesUploadedToast()
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Успех";
            toast.CloseText = "Закрыть";
            toast.Message = "Файл успешно загружен!";
            toast.Delay_ms = 20000;
            toast.BgColor = Colors.white;
            toast.HeadColor = Colors.success;
            toast.HeadTextColor = Colors.white;
            toast.BodyTextColor = Colors.dark;
            toast.CloseLinkColor = Colors.white;
            return PartialView("Partial_Toast", toast);
        }
        public ActionResult Partial_UpdatedToast()
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Успех";
            toast.CloseText = "Закрыть";
            toast.Message = "Успешно обновлено!";
            toast.Delay_ms = 20000;
            toast.BgColor = Colors.white;
            toast.HeadColor = Colors.primary;
            toast.HeadTextColor = Colors.white;
            toast.BodyTextColor = Colors.dark;
            toast.CloseLinkColor = Colors.white;
            return PartialView("Partial_Toast", toast);
        }
        public ActionResult Partial_DeletedToast()
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Успех";
            toast.CloseText = "Закрыть";
            toast.Message = "Успешно удалено!";
            toast.Delay_ms = 20000;
            toast.BgColor = Colors.white;
            toast.HeadColor = Colors.warning;
            toast.HeadTextColor = Colors.dark;
            toast.BodyTextColor = Colors.dark;
            toast.CloseLinkColor = Colors.dark;
            return PartialView("Partial_Toast", toast);
        }
        public ActionResult Partial_UnknownErrorToast()
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Ошибка";
            toast.CloseText = "Закрыть";
            toast.Message = "Непредвиденная ошибка!";
            toast.Delay_ms = 20000;
            toast.BgColor = Colors.warning;
            toast.HeadColor = Colors.danger;
            toast.HeadTextColor = Colors.white;
            toast.BodyTextColor = Colors.dark;
            toast.CloseLinkColor = Colors.warning;
            return PartialView("Partial_Toast", toast);
        }

        public ActionResult Partial_FileAlreadyExistsToast()
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Ошибка";
            toast.CloseText = "Закрыть";
            toast.Message = "Такой файл уже существует!";
            toast.Delay_ms = 20000;
            toast.BgColor = Colors.warning;
            toast.HeadColor = Colors.danger;
            toast.HeadTextColor = Colors.white;
            toast.BodyTextColor = Colors.dark;
            toast.CloseLinkColor = Colors.warning;
            return PartialView("Partial_Toast", toast);
        }

        public ActionResult Partial_ExErrorToast(string ex)
        {
            ToastModel toast = new ToastModel();
            toast.Head = "Ошибка";
            toast.CloseText = "Закрыть";
            toast.Message = ex;
            toast.IsAutohide = false;
            toast.BgColor = Colors.warning;
            toast.HeadColor = Colors.danger;
            toast.HeadTextColor = Colors.white;
            toast.BodyTextColor = Colors.white;
            toast.CloseLinkColor = Colors.warning;
            return PartialView("Partial_Toast", toast);
        }

        public ActionResult Partial_ToastValidation(string allErrors)
        {
            string[] errors = new string[] { };
            if (!string.IsNullOrEmpty(allErrors))
                errors = allErrors.Split('`');

            return PartialView(errors);
        }
    }
}