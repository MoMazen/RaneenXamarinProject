using SQLite;

namespace RaneenXamarinProject.Models
{
    public class ClientData
    {
        [PrimaryKey]
        public string Token { get; set; }
        public string wishList { get; set; }
        public string cartItems { get; set; }
    }
}
