using System;


namespace CatanAPI.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string FromId { get; set; }
        public virtual User From { get; set; }
        public string ToId { get; set; }
        public virtual User To { get; set; }

    }
}
