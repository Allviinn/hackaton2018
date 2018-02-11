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

        public ActionResult EditParcelle(HomeModel model)
        {
            Context context = CreateContext();

            try
            {
                this.Services.EditParcelle(model.Parcelle);

                this.AddSuccess("La parcelle est bien modifiée!");
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
                this.Services.DeleteParcelle(id);
                this.AddSuccess("La parcelle a bien été supprimée!");
                return this.RedirectToAction(nameof(HomeController.ListParcelles));
            }
            catch(Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListParcelles));
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

        public ActionResult ListEvenements(int id)
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel()
                {
                    Evenements = this.Services.GetEvenementByParcelle(id).ToList(),
                    IdParcelle = id
                };

                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListParcelles));
            }
        }

        public ActionResult AddEvenement(int id, int idParcelle)
        {
            Context context = CreateContext();

            try
            {
                HomeModel model = new HomeModel()
                {
                    Evenement = id != 0 ? this.Services.GetEvenement(id) : null,
                    IdParcelle = idParcelle
                };
                model.LoadEvenementParcelle(this.Services.GetEvenementParcelles());

                if (model.Evenement == null)
                {
                    model.Evenement = new Evenement { ParcelleId = idParcelle };
                    this.View(model);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.ListParcelles));
            }
        }

        [HttpPost]
        public ActionResult AddEvenement(HomeModel model)
        {
            Context context = CreateContext();

            try
            {
                if (model.Evenement.IdEvenement == 0)
                {
                    this.Services.AddEvenement(new Evenement
                    {
                        ParcelleId = model.IdParcelle,
                        EvenementParcelleId = model.EvenementParcelle.IdEvenementParcelle
                    });

                    this.AddSuccess("L'évenement a bien été ajouté!");
                    return this.RedirectToAction(nameof(HomeController.ListEvenements), new { id = model.IdParcelle });
                }

                this.Services.EditEvenement(new Evenement
                {
                    IdEvenement = model.Evenement.IdEvenement,
                    ParcelleId = model.IdParcelle,
                    EvenementParcelleId = model.EvenementParcelle.IdEvenementParcelle
                });

                this.AddSuccess("L'évenement a bien été édité!");
                return this.RedirectToAction(nameof(HomeController.ListEvenements), new { id = model.IdParcelle });
            }
            catch (Exception ex)
            {
                this.AddError(context.ErrorMessage, ex);
                return this.RedirectToAction(nameof(HomeController.Index));
            }
        }
    }
}