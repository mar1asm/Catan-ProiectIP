namespace CatanAPI.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public bool Accepted { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public User Receiver { get; set; }
        public int ReceiverId { get; set; }
    }
}