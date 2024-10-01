namespace Shop.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime CheckOutDate { get; set; }
        public string Address { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public int TotalPrice { get; set; }
        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }
    }
}
