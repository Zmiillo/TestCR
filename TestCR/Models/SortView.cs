namespace TestCR.Models
{
    public class SortView
    {
        public SortState NameSort { get; private set; } 
        public SortState SurnameSort { get; private set; }    
        public SortState LoginSort { get; private set; }   
        public SortState EmailSort { get; private set; }   
        public SortState Current { get; private set; }   
 
        public SortView(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            SurnameSort = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            LoginSort = sortOrder == SortState.LoginAsc ? SortState.LoginDesc : SortState.LoginAsc;
            EmailSort = sortOrder == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            Current = sortOrder;
        }
    }
}