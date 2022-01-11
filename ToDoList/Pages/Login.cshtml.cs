using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Model;

namespace ToDoList.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        [BindProperty]
        public Login Model { get; set; }
        public void OnGet()
        {
        }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            if(ModelState.IsValid)
            {
                
                var identityResult = await signInManager.PasswordSignInAsync(Model.Username, Model.Password, Model.RememberMe, false);
                if(identityResult.Succeeded)
                {
                    TempData["Success"] = "Logged In Success";
                    return RedirectToPage("Index");
                }

;            }
            TempData["Failure"] = "Logged In Failed";
            return Page();
        }
    }
}
