using System.Web.Mvc;

namespace InfTeh_test.Controllers
{
    public class ModalController : Controller
    {
        public ActionResult GetModal()
        {
            return PartialView("_PartialModal");
        }
    }
}