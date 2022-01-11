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
    public class DetailsModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;

        public DetailsModel(ToDoList.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Model.List List { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List = await _context.Lists.FirstOrDefaultAsync(m => m.Id == id);

            if (List == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
