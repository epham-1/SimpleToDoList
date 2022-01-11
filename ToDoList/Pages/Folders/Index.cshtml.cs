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
    public class IndexModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public IndexModel(ToDoList.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public IList<Model.Folders> Folders { get;set; }

        public async Task OnGetAsync()
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            Folders = await _context.Folder.Where(f => f.UserId == user.Id).ToListAsync();
        }
    }
}
