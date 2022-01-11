#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;

namespace ToDoList.Pages.List
{
    public class EditModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> userManager;

		public EditModel(ToDoList.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
			this.userManager = userManager;
		}

        [BindProperty]
        public Model.List List { get; set; }
        public int currentFolder { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List = await _context.Lists.FirstOrDefaultAsync(m => m.Id == id);
            currentFolder = List.FolderId;
            if (List == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            _context.Attach(List).State = EntityState.Modified;

            try
            {
                

                var user = await userManager.FindByEmailAsync(User.Identity.Name);
                List.UserId = user.Id;

;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(List.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { folderId=List.FolderId});
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
