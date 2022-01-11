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

namespace ToDoList.Pages.Folders
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
        public Model.Folders Folders { get; set; }
        public IList<Model.List> List { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Folders = await _context.Folder.FirstOrDefaultAsync(m => m.Id == id);
            if (Folders == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Update the Folder
            _context.Attach(Folders).State = EntityState.Modified;
            //Find user
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            Folders.UserId = user.Id;
            
            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoldersExists(Folders.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoldersExists(int id)
        {
            return _context.Folder.Any(e => e.Id == id);
        }
    }
}
