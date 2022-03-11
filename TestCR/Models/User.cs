using System.ComponentModel.DataAnnotations;

namespace TestCR.Models
{
    public enum SortState
    {
        NameAsc, NameDesc,  
        SurnameAsc, SurnameDesc,   
        LoginAsc, LoginDesc, 
        EmailAsc, EmailDesc 
    }
    public class User
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }
        [Required (ErrorMessage = "Пустой логин")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Логин слишком короткий или слишком длинный")]
        public string Login { get; set; }
        [Required (ErrorMessage = "Не заполнен E-mail")]
        [EmailAddress(ErrorMessage = "Неправильный электронный адрес")]
        public string Email { get; set; }
    }
    
}