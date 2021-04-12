using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace InfTeh_test.Models.DataContext
{
    [Table("Folder")]
    public class Folder
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int folderid { get; set; }

        [Required(ErrorMessage = "Обязательно укажите название!")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Название папки должно быть от 1 до 100 символов")]
        public string displayname { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? parent_folderid { get; set; }

        //

        [ForeignKey("parent_folderid")]
        public Folder ParentFolder { get; set; }

        // 

        [NotMapped]
        public IQueryable<Folder> ChildrenFolders { get; set; }

        [NotMapped]
        public IQueryable<File> ChildrenFiles { get; set; }
    }
}