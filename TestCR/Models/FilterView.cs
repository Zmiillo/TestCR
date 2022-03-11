using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestCR.Models
{
    public class FilterView
    {
        public FilterView(List<User> users, int? user, string name)
        {
            users.Insert(0, new User { Name = "Все", Id = 0 });
            Users = new SelectList(users, "Id", "Name", user);
            SelectedUser = user;
            SelectedName = name;
        }
        public SelectList Users { get; private set; } // список компаний
        public int? SelectedUser { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}