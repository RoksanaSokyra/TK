using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Entities;
using WebApplication5.Helpers;

namespace WebApplication5.Services
{
    public interface IContactService
    {
        void AddToContactList(int id, string username);
        ICollection<Contact> GetContactList(int id);
    }
    public class ContactService : IContactService
    {
        private DataContext _context;

        public ContactService(DataContext context)
        {
            _context = context;
        }
        public void AddToContactList(int id, string username)
        {
            var userInviting = _context.Users.Include(user => user.Contacts).SingleOrDefault(x => x.ID == id);
            var userInvited = _context.Users.Include(user => user.Contacts).SingleOrDefault(x => x.Username == username);
            if (userInviting == null || userInvited == null)
                throw new AppException("User not found");
            if (userInviting.Contacts.FirstOrDefault(user => user.Username == userInvited.Username) != null)
                throw new AppException("User already added");
            Contact contactUserInviting = new Contact() { FirstName = userInvited.FirstName, LastName = userInvited.LastName, Username = userInvited.Username };
            Contact contactUserInvited = new Contact() { FirstName = userInviting.FirstName, LastName = userInviting.LastName, Username = userInviting.Username };
            userInviting.Contacts.Add(contactUserInviting);
            userInvited.Contacts.Add(contactUserInvited);

            Conversation conv = new Conversation() { Messages = new List<Message>(), Contacts = new List<Contact>() {
            contactUserInviting, contactUserInvited
            } };
            _context.Conversations.Add(conv);
            _context.SaveChanges();
        }

        public ICollection<Contact> GetContactList(int id)
        {
            var user = _context.Users.Include(user => user.Contacts).SingleOrDefault(x => x.ID == id).Contacts;
            if (user == null)
                throw new AppException("User not found");
            return user;
        }
    }
}
