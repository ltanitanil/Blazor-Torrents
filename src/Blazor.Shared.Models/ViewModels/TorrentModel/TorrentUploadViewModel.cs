using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blazor.Shared.Models.ViewModels.TorrentModel
{
    public class TorrentUploadViewModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "Длина заголовка не может быть более 256 символов.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Контент не может быть более 2000 символов.")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [StringLength(256, ErrorMessage = "Название директории не может быть более 256 символов.")]
        [Display(Name = "DirName")]
        public string DirName { get; set; }

        [Required]
        [Display(Name = "SubcategoryId")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Выберите действительную подкатегорию.")]
        public int SubcategoryId { get; set; }
    }
}


