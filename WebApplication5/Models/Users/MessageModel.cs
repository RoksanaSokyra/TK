using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Users
{
    public class MessageModel
    {
        public int ID { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
