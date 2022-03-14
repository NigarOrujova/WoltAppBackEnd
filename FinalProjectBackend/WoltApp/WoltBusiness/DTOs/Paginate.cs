using System;
using System.Collections.Generic;
using System.Text;

namespace WoltBusiness.DTOs
{
    public class Paginate<T>
    {
        public Paginate(List<T> models, int currentPage, int pageCount)
        {
            Items = models;
            CurrentPage = currentPage;
            PageCount = pageCount;

        }
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
