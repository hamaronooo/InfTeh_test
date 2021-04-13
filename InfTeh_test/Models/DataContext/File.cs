using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace InfTeh_test.Models.DataContext
{
    [Table("File")]
    public class File
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int fileid { get; set; }

        [Required(ErrorMessage = "Обязательно укажите название!")]
        [StringLength(100, MinimumLength = 1,
            ErrorMessage = "Название файла должно быть от 1 до 200 символов")]
        public string displayname { get; set; }

        [StringLength(1000, MinimumLength = 3,
            ErrorMessage = "Описание файла должно быть от 3 до 1000 символов")]
        public string description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? file_extensionid { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? folderid { get; set; }

        //

        [ForeignKey("file_extensionid")]
        public FileExtension FileExtension { get; set; }
        
        [ForeignKey("folderid")]
        public Folder Folder { get; set; }

        //

        [NotMapped]
        public string IconFileName { get; set; } = "unknown.svg";
    }
}