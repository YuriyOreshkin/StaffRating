using StaffRating.Domain.Entities;
using System;
using System.Linq;

namespace StaffRating.Domain.Repository.Interfaces
{
    public interface IDBRepository : IDisposable
    {
        ICRUDRepository<CATEGORY> CATEGORIES { get;}
        ICRUDRepository<TEST> TESTS { get; }
        ICRUDRepository<QUESTION> QUESTIONS { get; }
        ICRUDRepository<ANSWER> ANSWERS { get; }
        ICRUDRepository<TESTDATES> TESTSDATES { get; }
        ICRUDRepository<TESTRATINGS> TESTRATINGS { get; }
    }
}
