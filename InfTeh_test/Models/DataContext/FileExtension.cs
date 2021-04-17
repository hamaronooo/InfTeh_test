using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace InfTeh_test.Models.DataContext
{
    [Table("FileExtension")]
    public class FileExtension
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int file_extensionid { get; set; }

        [Required(ErrorMessage = "Обязательно укажите название расширения!")]
        [StringLength(20, MinimumLength = 1,
            ErrorMessage = "Название расширения должно быть от 1 до 20 символов")]
        public string displayname { get; set; }

        [StringLength(200, MinimumLength = 1,
            ErrorMessage = "Название иконки должно быть от 1 до 200 символов")]
        public string icon_filename { get; set; }

        //
        
        

        [NotMapped]
        public List<string> IconsFilesNamesList;

    }
}