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

        public Parcelle GetParcelle(int id)
        {
            return this._context.Parcelles
                .Where(ep => ep.IdParcelle == id)
                .Select(parcelle => new Parcelle
                {
                    Adresse = parcelle.Adresse,
                    CreationDate = parcelle.CreationDate,
                    IdParcelle = parcelle.IdParcelle,
                    Lat = parcelle.Lat,
                    Lng = parcelle.Lng,
                    Nom = parcelle.Nom,
                    Ville = parcelle.Ville,
                    Evenements = parcelle.Evenements.Select(even => new Evenement
                    {
                        CreationDate = even.CreationDate,
                        EvenementParcelleId = even.EvenementParcelleId,
                        IdEvenement = even.IdEvenement,
                        ParcelleId = even.ParcelleId,
                        EvenementParcelle = new EvenementParcelle
                        {
                            Nom = even.EvenementParcelle.Nom,
                            Description = even.EvenementParcelle.Description,
                            IdEvenementParcelle = even.EvenementParcelle.IdEvenementParcelle,
                        }
                    })
                }).FirstOrDefault();
        }

        public IEnumerable<Parcelle> GetParcelles()
        {
            return this._context.Parcelles
                .Select(parcelle => new Parcelle
                {
                    Adresse = parcelle.Adresse,
                    CreationDate = parcelle.CreationDate,
                    IdParcelle = parcelle.IdParcelle,
                    Lat = parcelle.Lat,
                    Lng = parcelle.Lng,
                    Nom = parcelle.Nom,
                    Ville = parcelle.Ville,
                    Evenements = parcelle.Evenements.Select(even => new Evenement
                    {
                        CreationDate = even.CreationDate,
                        EvenementParcelleId = even.EvenementParcelleId,
                        IdEvenement = even.IdEvenement,
                        ParcelleId = even.ParcelleId,
                        EvenementParcelle = new EvenementParcelle
                        {
                            Nom = even.EvenementParcelle.Nom,
                            Description = even.EvenementParcelle.Description,
                            IdEvenementParcelle = even.EvenementParcelle.IdEvenementParcelle,
                        }
                    })
                });
        }

        public EvenementParcelle GetEvenementParcelle(int id)
        {
            return this._context.EvenementParcelles
                .Where(ep => ep.IdEvenementParcelle == id)
                .Select(evenparcelle => new EvenementParcelle
                {
                    Description = evenparcelle.Description,
                    Nom = evenparcelle.Nom,
                    IdEvenementParcelle = evenparcelle.IdEvenementParcelle,
                    Evenements = evenparcelle.Evenements.Select(even => new Evenement
                    {
                        CreationDate = even.CreationDate,
                        EvenementParcelleId = even.EvenementParcelleId,
                        IdEvenement = even.IdEvenement,
                        ParcelleId = even.ParcelleId,
                        Parcelle = new Parcelle
                        {
                            Adresse = even.Parcelle.Adresse,
                            CreationDate = even.Parcelle.CreationDate,
                            IdParcelle = even.Parcelle.IdParcelle,
                            Lat = even.Parcelle.Lat,
                            Lng = even.Parcelle.Lng,
                            Nom = even.Parcelle.Nom,
                            Ville = even.Parcelle.Ville,
                        }
                    })
                }).FirstOrDefault();
        }

        public IEnumerable<EvenementParcelle> GetEvenementParcelles()
        {
            return this._context.EvenementParcelles
                .Select(evenparcelle => new EvenementParcelle
                {
                    Description = evenparcelle.Description,
                    Nom = evenparcelle.Nom,
                    IdEvenementParcelle = evenparcelle.IdEvenementParcelle,
                    Evenements = evenparcelle.Evenements.Select(even => new Evenement
                    {
                        CreationDate = even.CreationDate,
                        EvenementParcelleId = even.EvenementParcelleId,
                        IdEvenement = even.IdEvenement,
                        ParcelleId = even.ParcelleId,
                        Parcelle = new Parcelle
                        {
                            Adresse = even.Parcelle.Adresse,
                            CreationDate = even.Parcelle.CreationDate,
                            IdParcelle = even.Parcelle.IdParcelle,
                            Lat = even.Parcelle.Lat,
                            Lng = even.Parcelle.Lng,
                            Nom = even.Parcelle.Nom,
                            Ville = even.Parcelle.Ville,
                        }
                    })
                });
        }

        public Evenement GetEvenement(int id)
        {
            return this._context.Evenements
                .Where(e => e.IdEvenement == id)
                .Select(even => new Evenement
            {
                CreationDate = even.CreationDate,
                EvenementParcelleId = even.EvenementParcelleId,
                IdEvenement = even.IdEvenement,
                ParcelleId = even.ParcelleId,
                EvenementParcelle = new EvenementParcelle
                {
                    Description = even.EvenementParcelle.Description,
                    Nom = even.EvenementParcelle.Nom,
                    IdEvenementParcelle = even.EvenementParcelle.IdEvenementParcelle
                },
                Parcelle = new Parcelle
                {
                    Adresse = even.Parcelle.Adresse,
                    CreationDate = even.Parcelle.CreationDate,
                    IdParcelle = even.Parcelle.IdParcelle,
                    Lat = even.Parcelle.Lat,
                    Lng = even.Parcelle.Lng,
                    Nom = even.Parcelle.Nom,
                    Ville = even.Parcelle.Ville
                }
            }).FirstOrDefault();
        }

        public IEnumerable<Evenement> GetEvenementByParcelle(int idParcelle)
        {
            return this._context.Evenements
                .Where(eve => eve.ParcelleId == idParcelle)
                .Select(even => new Evenement
            {
                CreationDate = even.CreationDate,
                EvenementParcelleId = even.EvenementParcelleId,
                IdEvenement = even.IdEvenement,
                ParcelleId = even.ParcelleId,
                EvenementParcelle = new EvenementParcelle
                {
                    Description = even.EvenementParcelle.Description,
                    Nom = even.EvenementParcelle.Nom,
                    IdEvenementParcelle = even.EvenementParcelle.IdEvenementParcelle
                },
                Parcelle = new Parcelle
                {
                    Adresse = even.Parcelle.Adresse,
                    CreationDate = even.Parcelle.CreationDate,
                    IdParcelle = even.Parcelle.IdParcelle,
                    Lat = even.Parcelle.Lat,
                    Lng = even.Parcelle.Lng,
                    Nom = even.Parcelle.Nom,
                    Ville = even.Parcelle.Ville
                }
            });
        }

        public IEnumerable<Evenement> GetEvenements()
        {
            return this._context.Evenements.Select(even => new Evenement
            {
                CreationDate = even.CreationDate,
                EvenementParcelleId = even.EvenementParcelleId,
                IdEvenement = even.IdEvenement,
                ParcelleId = even.ParcelleId,
                EvenementParcelle = new EvenementParcelle
                {
                    Description = even.EvenementParcelle.Description,
                    Nom = even.EvenementParcelle.Nom,
                    IdEvenementParcelle = even.EvenementParcelle.IdEvenementParcelle
                },
                Parcelle = new Parcelle
                {
                    Adresse = even.Parcelle.Adresse,
                    CreationDate = even.Parcelle.CreationDate,
                    IdParcelle = even.Parcelle.IdParcelle,
                    Lat = even.Parcelle.Lat,
                    Lng = even.Parcelle.Lng,
                    Nom = even.Parcelle.Nom,
                    Ville = even.Parcelle.Ville
                }
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

        public int AddUser(User user)
        {
            return DAL.ManageData<DAL.User>.Add(new DAL.User
            {
                CreationDate = DateTime.Now,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        public int AddParcelle(Parcelle parcelle)
        {
            return DAL.ManageData<DAL.Parcelle>.Add(new DAL.Parcelle
            {
                CreationDate = DateTime.Now,
                Adresse = parcelle.Adresse,
                Lat = parcelle.Lat,
                Lng = parcelle.Lng,
                Nom = parcelle.Nom,
                Ville = parcelle.Ville
            });
        }

        public int AddEvenementParcelle(EvenementParcelle evenementParcelle)
        {
            return DAL.ManageData<DAL.EvenementParcelle>.Add(new DAL.EvenementParcelle
            {
                Nom = evenementParcelle.Nom,
                Description = evenementParcelle.Description
            });
        }

        public int AddEvenement(Evenement evenement)
        {
            return DAL.ManageData<DAL.Evenement>.Add(new DAL.Evenement
            {
                CreationDate = DateTime.Now,
                EvenementParcelleId = evenement.EvenementParcelleId,
                ParcelleId = evenement.ParcelleId
            });
        }

        public int EditParcelle(Parcelle parcelle)
        {
            DAL.Parcelle par = DAL.ManageData<DAL.Parcelle>.Get(parcelle.IdParcelle);
            par.Adresse = parcelle.Adresse;
            par.CreationDate = parcelle.CreationDate;
            par.Lat = parcelle.Lat;
            par.Lng = parcelle.Lng;
            par.Nom = parcelle.Nom;
            par.Ville = parcelle.Ville;

            return DAL.ManageData<DAL.Parcelle>.Update(par);
        }

        public int EditEvenementParcelle(EvenementParcelle evenementParcelle)
        {
            DAL.EvenementParcelle evenement = DAL.ManageData<DAL.EvenementParcelle>.Get(evenementParcelle.IdEvenementParcelle);
            evenement.Description = evenementParcelle.Description;
            evenement.Nom = evenementParcelle.Nom;

            return DAL.ManageData<DAL.EvenementParcelle>.Update(evenement);
        }

        public int EditEvenement(Evenement evenement)
        {
            DAL.Evenement even = DAL.ManageData<DAL.Evenement>.Get(evenement.IdEvenement);
            even.EvenementParcelleId = evenement.EvenementParcelleId;
            even.ParcelleId = evenement.ParcelleId;

            return DAL.ManageData<DAL.Evenement>.Update(even);
        }

        public int DeleteParcelle(int id)
        {
            return DAL.ManageData<DAL.Parcelle>.Delete(id);
        }

        public int DeleteEvenementParcelle(int id)
        {
            return DAL.ManageData<DAL.EvenementParcelle>.Delete(id);
        }

        public int DeleteEvenement(int id)
        {
            return DAL.ManageData<DAL.Evenement>.Delete(id);
        }
    }
}
