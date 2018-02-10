using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.BE;

namespace SP.BLL
{
    public class Services
    {
        private DAL.DbSuiviParcelleEntities1 _context;
        public Services()
        {
            this._context = new DAL.DbSuiviParcelleEntities1();
        }

        public User GetUserById(int id)
        {
            return this._context.Users
                .Where(u => u.Id == id)
                .Select(u => new BE.User
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return this._context.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            return this._context.Users
                .Where(u => u.Login == login && u.Password == password)
                .Select(u => new User
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).FirstOrDefault();
        }

        public int AddUser(string login, string password, string firstName, string lastName)
        {
            return DAL.ManageData<DAL.User>.Add(new DAL.User
            {
                CreationDate = DateTime.Now,
                Login = login,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            });
        }
    }
}
