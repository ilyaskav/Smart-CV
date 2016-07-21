using Entities.Classes;
using Repository.Interfaces;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
{
    public class ContactService : IContactService
    {
        #region Declarations

        private readonly IContactRepository _contactRepository = null;
        private readonly IContactTitleRepository _contactTitleRepository = null;

        #endregion

        public ContactService(IContactRepository contRep, IContactTitleRepository contTitleRep)
        {
            _contactRepository = contRep;
            _contactTitleRepository = contTitleRep;
        }

        public void CreateContact(ContactModel model)
        {
            int contTitleId;
            if (_contactTitleRepository.Has(e => e.Title.Equals(model.ContactTitle)))
            {
                var entity=_contactTitleRepository.Get(e => e.Title.Equals(model.ContactTitle)).FirstOrDefault();
                contTitleId = entity.Id;
            }
            else contTitleId = _contactTitleRepository.Add(new ContactTitle() { Title=model.ContactTitle.Title });

            _contactRepository.Add(new Contact() { ContactTitleId = contTitleId, Data = model.Data });
        }

        public void RemoveContact(int id)
        {
            if (_contactRepository.Has(id)) _contactRepository.Remove(id);
        }

        public void RemoveContact(ContactModel model)
        {
            if (_contactRepository.Has(model.Id)) _contactRepository.Remove(model.Id);
        }

        public void Dispose()
        {
            _contactTitleRepository.Dispose();
            _contactRepository.Dispose();
        }

        public void UpdateContact(ContactModel model)
        {
            if (_contactRepository.Has(model.Id))
            {
                var entity = _contactRepository.Get(model.Id);
                entity.Data = model.Data;
                if (!entity.ContactTitle.Equals(model.ContactTitle))
                {
                    int contTitleId;
                    if (_contactTitleRepository.Has(e => e.Title.Equals(model.ContactTitle)))
                    {
                        var title = _contactTitleRepository.Get(e => e.Title.Equals(model.ContactTitle)).FirstOrDefault();
                        contTitleId = title.Id;
                    }
                    else contTitleId = _contactTitleRepository.Add(new ContactTitle() { Title = model.ContactTitle.Title });

                    entity.ContactTitleId = contTitleId;
                }
                _contactRepository.Update(entity);
            }
        }
    }
}
