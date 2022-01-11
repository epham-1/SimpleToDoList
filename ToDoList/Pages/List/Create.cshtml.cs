#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Data;
using ToDoList.Model;

namespace ToDoList.Pages.List
{
    public class CreateModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> userManager;

		public CreateModel(ToDoList.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
			this.userManager = userManager;
		}

        public IActionResult OnGet(string? folderName)
        {
            currentFolder = folderName;
            return Page();
        }

        [BindProperty]
        public Model.List List { get; set; }
        public string currentFolder { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int folderId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            List.UserId = user.Id;
            List.FolderId = folderId;
            _context.Lists.Add(List);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index",new {folderId=folderId });
        }
    }
}
