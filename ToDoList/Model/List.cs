using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public class List
    {
        //Task Id
        [Key]
        public int Id { get; set; }
        //ID of the User
        public string UserId { get; set; } = "";
        [Required]
        [MaxLength(75)]
        public string Title { get; set; } = "";
        [Required]
        [MaxLength(150)]
        public string Desc { get; set; } = "";
        public bool Completed { get; set; }
        public int FolderId { get; set; }
    }
}
