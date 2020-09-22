using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WebApplication5.Entities
{
    public class Message
    {
        [IgnoreDataMember] 
        public int ID { get; set; }
        [IgnoreDataMember] 
        public int ConversationID { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string Content { get; set; }

        [IgnoreDataMember] 
        public DateTime TimeStamp { get; set; }

        [IgnoreDataMember]        
        public virtual Conversation Conversation { get; set; }
    }
}
