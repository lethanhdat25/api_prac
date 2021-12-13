using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2008M_APICore.Models;
namespace T2008M_APICore.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task UpdateEmployeeAsync(Employee employee);
        Task SaveChangesAsync();
    }
}
