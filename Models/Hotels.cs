namespace Shop.Models
{
    public enum Country
    {
        Azerbaycan,
        Japan,
        Amerika,
        Turkey
    }
    public class Hotels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int PeopleCount { get; set; }
        public int RoomCount { get; set; }
        public decimal Rating { get; set; }
        public Country Country { get; set; }
        public int HotelCategoriesId { get; set; }
        public HotelCatigories HotelCategories { get; set; } 
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Orders> Orders { get; set; }

    }

    public class HotelsDTOModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int PeopleCount { get; set; }
        public int RoomCount { get; set; }
        public decimal Rating { get; set; }
        public Country Country { get; set; }
        public int HotelCategoriesId { get; set; }
    }
    public class HotelCategoryViewModel
    {
        public List<Hotels> Hotels { get; set; }
        public List<HotelCatigories> Categories { get; set; }
    }

    public class HotelCategoryFilterViewModel
    {
        public List<Hotels> Hotels { get; set; }
        public List<HotelCatigories> Categories { get; set; }
        
    }

   

}