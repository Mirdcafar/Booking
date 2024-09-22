namespace Shop.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string logoImage { get; set; }  
        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }

 
    }
}
