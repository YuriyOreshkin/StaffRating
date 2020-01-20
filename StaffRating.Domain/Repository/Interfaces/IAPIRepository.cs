using StaffRating.Domain.Entities;
using System.Linq;

namespace StaffRating.Domain.Repository.Interfaces
{
    public interface IAPIRepository
    {
        IQueryable<Employee> GetEmployees(string filter);
        Employee GetEmployee(long id);
        Employee GetEmployee(string login);

        IQueryable<TreeViewEmployee> GetEmployeesWithDepartments(string filter);
    }

}
