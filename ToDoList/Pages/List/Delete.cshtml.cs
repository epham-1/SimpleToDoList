#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;

namespace ToDoList.Pages.List
{
    public class DeleteModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;

        public DeleteModel(ToDoList.Data.ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List = await _context.Lists.FindAsync(id);
            currentFolder = List.FolderId;
            if (List != null)
            {
                _context.Lists.Remove(List);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { folderId = currentFolder });
        }
    }
}
