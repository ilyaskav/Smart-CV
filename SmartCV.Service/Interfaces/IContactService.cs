using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface IContactService : IDisposable
    {
        ContactAddModel Get(int managerId);

        void CreateContact(ContactModel model);

        bool UpdateContact(ContactModel model);

        void UpdateContact(ContactAddModel addModel);

        void RemoveContact(ContactModel model);

        void RemoveContact( int contactId);
    }
}
