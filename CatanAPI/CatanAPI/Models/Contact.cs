namespace CatanAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public bool Accepted { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}