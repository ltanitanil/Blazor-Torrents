using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PaginationInfoViewModel
    {
        public int TotalTorrents { get; set; }//Всего торрентов
        public int TorrentsPerPage { get; set; }//Торренты на странице
        public int ActualPage { get; set; }//Текущая страница
        public int TotalPages { get; set; }//всего страниц
        public string Previous { get; set; }//предыдущая
        public string Next { get; set; }//следующая
    }
}