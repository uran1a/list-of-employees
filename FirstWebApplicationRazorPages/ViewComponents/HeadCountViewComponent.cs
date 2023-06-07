using FirstWebApplicationRazorPages.Models;
using FirstWebApplicationRazorPages.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplicationRazorPages.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IViewComponentResult Invoke(Dept? department = null)
        {
            var result = _employeeRepository.EmployeeCountByDept(department);
            return View(result);
        }
    }
}
