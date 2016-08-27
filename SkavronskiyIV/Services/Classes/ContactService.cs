using Entities.Classes;
using Repository.Interfaces;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Converters;

namespace Services.Classes
{
    public class ContactService : IContactService
    {
        #region Declarations

        private readonly IContactRepository _contactRepository = null;
        private readonly IContactTitleRepository _contactTitleRepository = null;
        private readonly IResumeManagerRepository _managerRepository = null;
        private readonly IResumeRepository _resumeRepository = null;

        #endregion

        public ContactService(IContactRepository contRep, IContactTitleRepository contTitleRep, IResumeManagerRepository manRep, IResumeRepository resumeRep)
        {
            _contactRepository = contRep;
            _contactTitleRepository = contTitleRep;
            _managerRepository = manRep;
            _resumeRepository = resumeRep;
        }

        public ContactAddModel Get(int managerId)
        {
            var resumeManager = _managerRepository.Get(managerId);
            var model = resumeManager.Resume.Contacts.ToAddModel();

            return model;
        }
        public void CreateContact(ContactModel model)
        {
            //int contTitleId;
            if (_contactTitleRepository.Has(e => e.Title.Equals(model.ContactTitle.Title)))
            {
                var entity=_contactTitleRepository.Get(e => e.Title.Equals(model.ContactTitle.Title)).FirstOrDefault();
                model.ContactTitle.Id = entity.Id;
            }
            else model.ContactTitle.Id = _contactTitleRepository.Add(new ContactTitle() { Title=model.ContactTitle.Title });

            _contactRepository.Add(model.ToEntity());
        }

        public void RemoveContact(int id)
        {
            if (_contactRepository.Has(id)) _contactRepository.Remove(id);
        }

        public void RemoveContact(ContactModel model)
        {
            if (_contactRepository.Has(model.Id.Value)) _contactRepository.Remove(model.Id.Value);
        }

        public void Dispose()
        {
            _contactTitleRepository.Dispose();
            _contactRepository.Dispose();
            _managerRepository.Dispose();
        }

        public bool UpdateContact(ContactModel model)
        {
            if (!model.Id.HasValue) return false;
            if (_contactRepository.Has(model.Id.Value))
            {
                var entity = _contactRepository.Get(model.Id.Value);
                entity.Data = model.Data;
                if (!entity.ContactTitle.Equals(model.ContactTitle))
                {
                    int contTitleId;
                    if (_contactTitleRepository.Has(e => e.Title.Equals(model.ContactTitle.Title)))
                    {
                        var title = _contactTitleRepository.Get(e => e.Title.Equals(model.ContactTitle.Title)).FirstOrDefault();
                        contTitleId = title.Id;
                    }
                    else contTitleId = _contactTitleRepository.Add(new ContactTitle() { Title = model.ContactTitle.Title });

                    entity.ContactTitleId = contTitleId;
                }
                return _contactRepository.Update(entity);
            }
            return false;
        }

        public void UpdateContact(ContactAddModel addModel)
        {
            var resume=_resumeRepository.Get(addModel.ResumeManagerId.Value);
            foreach (var contact in addModel.Contacts)
            {
                // обновить контакт если такой есть
                if (!this.UpdateContact(contact))
                    // создать новый если нет
                    this.CreateContact(contact);
            }

            if (resume.Contacts.Count == 0)
            {
                this.CreateContact(new ContactModel() { Data = addModel.EMail, ContactTitle = _contactTitleRepository.Get(t => t.Title.Equals("EMail")).FirstOrDefault().ToModel(), ResumeId = resume.Id });
                this.CreateContact(new ContactModel() { Data = addModel.Phone, ContactTitle = _contactTitleRepository.Get(t => t.Title.Equals("Phone")).FirstOrDefault().ToModel(), ResumeId = resume.Id });
                //contacts.Add(new Contact() { Data = addModel.EMail, ContactTitle = _contactTitleRepository.Get(t => t.Title.Equals("EMail")).FirstOrDefault() });
                //contacts.Add(new Contact() { Data = addModel.Phone, ContactTitle = _contactTitleRepository.Get(t => t.Title.Equals("Phone")).FirstOrDefault() });
            }
            else
            {
                var email = resume.Contacts.FirstOrDefault(e => e.ContactTitle.Title.Equals("EMail"));
                    email.Data = addModel.EMail;
                    _contactRepository.Update(email);

                var phone = resume.Contacts.FirstOrDefault(e => e.ContactTitle.Title.Equals("Phone"));
                    phone.Data = addModel.Phone;
                    _contactRepository.Update(phone);
            }
            
        }
    }
}
