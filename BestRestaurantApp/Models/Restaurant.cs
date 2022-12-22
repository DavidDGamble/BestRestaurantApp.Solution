namespace BestRestaurantApp.Models {
    public class Restaurant {
        public string RestaurantId {get; set;}
        public string Name {get; set;}
        public string Address {get; set;}
         public string Rating {get; set;}
        public int CuisineId {get; set;}
        public Cuisine Cuisine { get; set; }
    }
    
}