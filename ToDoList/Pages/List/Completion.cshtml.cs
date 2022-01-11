using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoList.Pages.List
{
    public class CompletionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CompletionModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Model.List List { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Changes the completion state
            List = await _context.Lists.FirstOrDefaultAsync(m => m.Id == id);
            List.Completed = !List.Completed;
            await _context.SaveChangesAsync();

            //Gets the foldername
            var currentFolder = List.FolderId;

            if (List == null)
            {
                return NotFound();
            }
            return RedirectToPage("./Index", new { folderId=currentFolder});
        }
    }
}
