using FirstWebApplicationRazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWebApplicationRazorPages.Services
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public SqlEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Employee Add(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
            {
                _context.Employees.Remove(employeeToDelete);
                _context.SaveChanges();
            }
            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> employees = _context.Employees;
            if (dept.HasValue)
                employees = employees.Where(e => e.Department == dept.Value);
            return employees.GroupBy(e => e.Department)
                            .Select(e => new DeptHeadCount()
                            {
                                Department = e.Key.Value,
                                Count = e.Count()
                            });
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _context.Employees;
            return _context.Employees.Where(e => e.Name.ToLower().Contains(searchTerm.ToLower()) || e.Email.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public Employee Update(Employee updatedEmployee)
        {
            var employee = _context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedEmployee;
        }
    }
}
