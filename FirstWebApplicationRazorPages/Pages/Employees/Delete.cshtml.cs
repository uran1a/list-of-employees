using FirstWebApplicationRazorPages.Models;
using FirstWebApplicationRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstWebApplicationRazorPages.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public Employee Employee { get; set; }

        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound");
            return Page();
        }
        public IActionResult OnPost()
        {
            Employee deletedEmployee = _employeeRepository.Delete(Employee.Id);
            if (deletedEmployee.PhotoPath != null)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", deletedEmployee.PhotoPath);
                if (deletedEmployee.PhotoPath != "noimage.png")
                    System.IO.File.Delete(filePath);
            }
            if (deletedEmployee == null)
                return RedirectToPage("/NotFound");
            return RedirectToPage("/Employees/Employees");
        }
    }
}
