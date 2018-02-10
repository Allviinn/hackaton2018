using System.Collections.Generic;

namespace SP.DAL
{
    public class ManageData<T> where T : class
    {
        static DbSuiviParcelleEntities1 _context = new DbSuiviParcelleEntities1();

        public static int Add(T element)
        {
            _context.Entry<T>(element).State = System.Data.Entity.EntityState.Added;
            return _context.SaveChanges();
        }

        public static int Delete(int id)
        {
            var res = Get(id);
            _context.Entry<T>(res).State = System.Data.Entity.EntityState.Deleted;
            return _context.SaveChanges();
        }

        public static int Update(T element)
        {
            _context.Entry<T>(element).State = System.Data.Entity.EntityState.Modified;
            return _context.SaveChanges();
        }

        public static T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public static T Get(T element)
        {
            return _context.Set<T>().Find(element);
        }

        public static List<T> Get(List<int> lId)
        {
            List<T> lObj = new List<T>();
            lId.ForEach(
                t =>
                {
                    var res = Get(t);
                    lObj.Add(res);
                });
            return lObj;
        }
    }
}
