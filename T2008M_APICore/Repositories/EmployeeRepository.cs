using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2008M_APICore.Models;

namespace T2008M_APICore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private T2008M_ASPContext context;

        public EmployeeRepository(T2008M_ASPContext context)
        {
            this.context = context;
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            Employee job = await context.Employees.FindAsync(id);
            context.Employees.Remove(job);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var employeeInDb = await context.Employees.FindAsync(employee.Id);
            employeeInDb = employee;
            await SaveChangesAsync();
        }
    }
}
