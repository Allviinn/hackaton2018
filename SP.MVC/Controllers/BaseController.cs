using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP.BE;

namespace SP.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected Context CreateContext()
        {
            this.TempData.Remove("error");
            var user = this.Request.Cookies["user"];

            return new Context() { ErrorMessage = new List<string>(), IdUser = user != null ? int.Parse(user.Value) : 0 };
        }

        public bool IsConnected()
        {
            var user = this.Request.Cookies["user"];
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddSuccess(string successMessage)
        {
            string str = string.Empty;
            Debug.WriteLine(string.Format("User Success: {0}", successMessage));
            str += string.Format("User Success: {0}<br />", successMessage);

            this.TempData.Remove("success");
            this.TempData.Add("success", str);
        }

        public void AddError(List<string> errorMassages, Exception ex)
        {
            string str = string.Empty;
            if (errorMassages.Any())
            {
                foreach (var error in errorMassages)
                {
                    Debug.WriteLine(string.Format("User Error: {0}", error));
                    str += string.Format("User Error: {0}<br />", error);
                }
            }

            Debug.WriteLine(string.Format("//Techincal Error: {0}", ex.Message));
            str += string.Format("//Techincal Error: {0}<br />", ex.Message);

            if (ex.InnerException != null)
            {
                Debug.WriteLine(string.Format("//Techincal Error: {0}", ex.InnerException.Message));
                str += string.Format("//Techincal Error: {0}", ex.InnerException.Message);
            }

            this.TempData.Remove("success");
            this.TempData.Add("error", str);
        }
    }
}