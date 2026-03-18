using Library.MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: /Admin/Roles
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            return View(roles);
        }

        // POST: /Admin/CreateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                TempData["Error"] = "Role name cannot be empty.";
                return RedirectToAction(nameof(Roles));
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                TempData["Error"] = "Role already exists.";
                return RedirectToAction(nameof(Roles));
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));

            if (result.Succeeded)
            {
                TempData["Success"] = "Role created successfully.";
            }
            else
            {
                TempData["Error"] = string.Join(" ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Roles));
        }

        // POST: /Admin/DeleteRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["Error"] = "Invalid role id.";
                return RedirectToAction(nameof(Roles));
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                TempData["Error"] = "Role not found.";
                return RedirectToAction(nameof(Roles));
            }

            if (role.Name == "Admin")
            {
                TempData["Error"] = "The Admin role cannot be deleted.";
                return RedirectToAction(nameof(Roles));
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["Success"] = "Role deleted successfully.";
            }
            else
            {
                TempData["Error"] = string.Join(" ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Roles));
        }
    }
}
