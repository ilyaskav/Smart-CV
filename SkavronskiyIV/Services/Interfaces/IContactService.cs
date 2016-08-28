using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
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
