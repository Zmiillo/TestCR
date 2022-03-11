using System.Collections.Generic;

namespace TestCR.Models
{
    public class IndexView
    {
        public IEnumerable<User> Users { get; set; }
        public FilterView FilterView{ get; set; }
        public SortView SortView { get; set; }
    }
}