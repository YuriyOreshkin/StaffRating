using StaffRating.Domain.Entities;
using StaffRating.Domain.Repository.Interfaces;
using System;
using System.Linq;

namespace StaffRating.Domain.Repository.Realizations.EF
{
    public class EFRepository : IDBRepository
    {
        private DBContext db = new DBContext();
        private bool disposed = false;


        public ICRUDRepository<CATEGORY> CATEGORIES => new EFCRUDRepository<CATEGORY>(db);
        public ICRUDRepository<TEST> TESTS => new EFCRUDRepository<TEST>(db);
        public ICRUDRepository<QUESTION> QUESTIONS => new EFCRUDRepository<QUESTION>(db);
        public ICRUDRepository<ANSWER> ANSWERS => new EFCRUDRepository<ANSWER>(db);

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
       
    }
}
