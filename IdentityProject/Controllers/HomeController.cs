using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityProject.Controllers {

    [Authorize]
    public class HomeController : Controller {

        [Authorize(Roles = ApplicationRoleManager.ROLE_ADMIN)]
        public ActionResult Index() {
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            if (User.Identity.Name != "eric@gmail.com") {
                return new HttpUnauthorizedResult();
            }

            return View();
        }
    }
}