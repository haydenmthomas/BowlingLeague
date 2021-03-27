using System;
namespace BowlingLeague.Models.ViewModels
{
    //This whole class just takes care of the page numbering
    public class PageNumbering
    {
        public decimal NumItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int NumPages => (int)(Math.Ceiling(TotalItems / NumItemsPerPage));
    }
}
