using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP.BE;
using SP.BLL;
using SP.MVC.Models;

namespace SP.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public Services Services { get; set; }
        public HomeController()
        {
            this.Services = new Services();
        }

        public ActionResult Index()
        {
            Context context = CreateContext();

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }

        public ActionResult About()
        {
            Context context = CreateContext();

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.About));
            }
        }

        public ActionResult EditEvenementParcelle(int id)
        {
            Context context = CreateContext();

            try
            {
                var evenementParcelle = this.Services.GetEvenementParcelle(id);
                EvenementParcelleModel model = new EvenementParcelleModel
                {
                    Description = evenementParcelle.Description,
                    Nom = evenementParcelle.Nom,
                    IdEvenementParcelle = evenementParcelle.IdEvenementParcelle
                };
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.EditEvenementParcelle));
            }
        }

        public ActionResult DeleteEvenementParcelle(int id)
        {
            Context context = CreateContext();

            try
            {
                var evenementParcelle = this.Services.GetEvenementParcelle(id);
                EvenementParcelleModel model = new EvenementParcelleModel
                {
                    Description = evenementParcelle.Description,
                    Nom = evenementParcelle.Nom,
                    IdEvenementParcelle = evenementParcelle.IdEvenementParcelle
                };
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.DeleteEvenementParcelle));
            }
        }

        public ActionResult AddEvenementParcelle()
        {
            Context context = CreateContext();

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.AddEvenementParcelle));
            }
        }

        public ActionResult ListEvenementsParcelle()
        {
            Context context = CreateContext();
            var evenementsParcelle = this.Services.GetEvenementParcelles().
                Select(ep => new Models.EvenementParcelleModel {
                    IdEvenementParcelle = ep.IdEvenementParcelle,
                    Description = ep.Description,
                    Nom = ep.Nom
                }).ToList();
            try
            {
                return View(evenementsParcelle);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListEvenementsParcelle));
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            Context context = CreateContext();
            try
            {
                HomeModel model = new HomeModel()
                {
                    User = new User()
                };
                return this.View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }

        [HttpPost]
        public ActionResult Login(HomeModel model)
        {
            Context context = CreateContext();
            try
            {
                User user = this.Services.GetUserByLoginPassword(model.User.Login, model.User.Password);
                if (user == null)
                {
                    context.ErrorMessage.Add("L'utilisateur n'existe pas ou le mot de passe est incorrect.");
                    throw new Exception();
                }

                HttpCookie userCookie = new HttpCookie("user")
                {
                    Value = user.Id.ToString(),
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpCookie firstnameCookie = new HttpCookie("firstname")
                {
                    Value = user.FirstName,
                    Expires = DateTime.Now.AddDays(1)
                };
                this.Response.Cookies.Add(userCookie);
                this.Response.Cookies.Add(firstnameCookie);

                this.AddSuccess("Success connection!");
                return this.RedirectToAction(nameof(HomeController.Index), new { });
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.View(model);
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            Context context = CreateContext();
            try
            {
                HomeModel model = new HomeModel()
                {
                    User = new User()
                };
                return this.View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }

        [HttpPost]
        public ActionResult SignUp(HomeModel model)
        {
            Context context = CreateContext();
            try
            {
                int message = this.Services.AddUser(model.User);

                return this.RedirectToAction(nameof(HomeController.Index));
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }
    }
}