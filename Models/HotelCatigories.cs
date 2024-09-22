namespace Shop.Models
{
    public class HotelCatigories
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Hotels> Hotels { get; set; }
    }
}
