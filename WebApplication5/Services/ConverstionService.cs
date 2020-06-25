﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Entities;
using WebApplication5.Helpers;

namespace WebApplication5.Services
{
    public interface IConversationService
    {
        public void SaveMessage(Message message);
        public ICollection<Message> getMessages(int id, string username);
    }
    public class ConversationService :  IConversationService
    {
        private DataContext _context;
        public ConversationService(DataContext context)
        {
            _context = context;
        }
        public void SaveMessage(Message message)
        {
            var conversation = _context.Conversations.Include(conv => conv.Messages).SingleOrDefault(x => x.ID == message.ConversationID);
            if (conversation == null)
                throw new AppException("Conversation not found");
            conversation.Messages.Add(message);
            _context.SaveChanges();
        }
        public ICollection<Message> getMessages(int id, string username)
        {
            var messages = _context.Users
                .Include(user => user.Contacts)
                .ThenInclude(contact => contact.Conversation)
                .ThenInclude(conversation => conversation.Messages)
                .SingleOrDefault(x => x.ID == id).Contacts.SingleOrDefault(x => x.Username == username)
                .Conversation.Messages;
            if (messages == null)
                throw new AppException("User not found");
            return messages;
        }
    }
}
