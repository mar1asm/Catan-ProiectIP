namespace CatanAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public bool Accepted { get; set; }

        public string SenderId;
        public virtual User Sender { get; set; }

        public string ReceiverId;
        public virtual User Receiver { get; set; }
        public void AddContactRequest(User user, User friendUser)
        {
            var friendRequest = new Contact()
            {
                Sender = user,
                Receiver = friendUser,
                Accepted = false
            };
            user.SentContactRequests.Add(friendRequest);
        }
    }
}