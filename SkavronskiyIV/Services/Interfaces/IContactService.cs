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

        void CreateContact(ContactModel model);

        void UpdateContact(ContactModel model);

        void RemoveContact(ContactModel model);

        void RemoveContact(int id);
    }
}
