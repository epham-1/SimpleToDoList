#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;

namespace ToDoList.Pages.Folders
{
    public class DeleteModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public DeleteModel(ToDoList.Data.ApplicationDbContext context,UserManager<IdentityUser> userManager)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Find the Folder to delete
            Folders = await _context.Folder.FindAsync(id);

            //Find all tasks in Folder to delete
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            List = await _context.Lists.Where(l => l.UserId==user.Id && l.FolderId==Folders.Id).ToListAsync();

            if (Folders != null)
            {
                foreach(var item in List)
                {
                    _context.Lists.Remove(item);
                }
                _context.Folder.Remove(Folders);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
