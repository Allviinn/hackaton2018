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

        #region EvenementParcelle

        public ActionResult AddEvenementParcelle()
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel();
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.AddEvenementParcelle));
            }
        }

        [HttpPost]
        public ActionResult AddEvenementParcelle(HomeModel model)
        {
            Context context = CreateContext();

            try
            {
                this.Services.AddEvenementParcelle(model.EvenementParcelle);

                this.AddSuccess("Evenement ajouté.");
                return View(model);
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

            try
            {
                HomeModel model = new HomeModel
                {
                    EvenementParcelles = this.Services.GetEvenementParcelles().ToList()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListEvenementsParcelle));
            }
        }

        public ActionResult EditEvenementParcelle(int? id)
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel
                {
                    EvenementParcelle = id.HasValue ? this.Services.GetEvenementParcelle(id.Value) : null
                };

                if (model.EvenementParcelle == null)
                {
                    model.EvenementParcelle = new EvenementParcelle();
                    return this.View(model);
                }

                return this.View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.EditEvenementParcelle));
            }
        }

        [HttpPost]
        public ActionResult EditEvenementParcelle(HomeModel model)
        {
            Context context = CreateContext();

            try
            {
                this.Services.EditEvenementParcelle(model.EvenementParcelle);

                this.AddSuccess("L'évenement est bien édité!");
                return this.View(model);
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
                this.AddSuccess("L'évenement a bien été supprimé");
                return this.RedirectToAction(nameof(HomeController.ListEvenementsParcelle));
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListEvenementsParcelle));
            }
        }

        #endregion EvenementParcelle

        #region Parcelle

        [HttpPost]
        public ActionResult AddParcelle(string name, string lat, string lng, string ville)
        {
            Context context = CreateContext();
            try
            {
                int message = this.Services.AddParcelle(new Parcelle
                {
                    Nom = name,
                    Lat = lat,
                    Lng = lng,
                    Ville = ville
                });

                this.AddSuccess("Parcelle ajoutée!");
                return this.RedirectToAction(nameof(HomeController.Index));
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }


       

        public ActionResult ListParcelles()
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel
                {
                    Parcelles = this.Services.GetParcelles().ToList()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListParcelles));
            }
        }

        public ActionResult EditParcelle(Parcelle parcelle)
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel
                {
                    Parcelle = this.Services.GetParcelle(parcelle.IdParcelle)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.EditParcelle));
            }
        }

        public ActionResult DeleteParcelle(int id)
        {
            Context context = CreateContext();

            try
            {
                return this.RedirectToAction(nameof(HomeController.DeleteParcelle));
            }
        }
        
        #endregion Parcelle

        #region User

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

                this.AddSuccess("Utilisateur ajouté!");
                return this.RedirectToAction(nameof(HomeController.Index));
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }

        #endregion User

        public ActionResult AddEvenement()
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel()
                {

                };
                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.AddEvenement));
            }
        }
    }
}