namespace Shop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Icon { get; set; } = "https://ukulell.ru/wp-content/uploads/2023/12/default_logo_user-395.jpg";
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } 
        public bool IsAdmin { get; set; } 
    }
}
