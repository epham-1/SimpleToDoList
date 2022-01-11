using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public class Folders
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        [Required]
        public string FolderName { get; set; }
    }
}
