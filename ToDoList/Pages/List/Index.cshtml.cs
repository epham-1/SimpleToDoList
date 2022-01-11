#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;

namespace ToDoList.Pages.List
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ToDoList.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ToDoList.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IList<Model.List> List { get;set; }
        public IList<Model.Folders> Folders { get; set; }
        public Model.Folders currentFolder { get; set; }

        public async Task<IActionResult> OnGetAsync(int folderId)
        {
            

            //Displays the current user's Tasks in the Folder
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List = await _context.Lists.Where(l => l.UserId == user.Id && l.FolderId==folderId).ToListAsync();
            currentFolder = await _context.Folder.FirstAsync(f => f.UserId == user.Id && f.Id == folderId);
            //Displays the current user's folders
            Folders = await _context.Folder.Where(f=>f.UserId==user.Id).ToListAsync();

            return Page();
        }
    }
}
