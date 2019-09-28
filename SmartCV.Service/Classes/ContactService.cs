using Microsoft.EntityFrameworkCore;
using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class ContactService : IContactService
    {
        #region Declarations

        private readonly IContactRepository _contactRepository = null;
        private readonly IContactTitleRepository _contactTitleRepository = null;
        private readonly IResumeRepository _resumeRepository = null;

        #endregion

        public ContactService(IContactRepository contRepo, IContactTitleRepository contTitleRepo, IResumeRepository resumeRepo)
        {
            _contactRepository = contRepo;
            _contactTitleRepository = contTitleRepo;
            _resumeRepository = resumeRepo;
        }

        public ContactAddModel Get(int managerId)
        {
            var contacts = _contactRepository.Get(c => c.ResumeId == managerId).Include(c=>c.ContactTitle).ToList();
            if (contacts == null || !contacts.Any()) return null;

            var model = contacts.ToAddModel();
            return model;
        }

        public void CreateContact(ContactModel model)
        {
            if (_contactTitleRepository.Has(e => e.Title.Equals(model.ContactTitle.Title)))
            {
                var entity = _contactTitleRepository.Get(e => e.Title.Equals(model.ContactTitle.Title)).FirstOrDefault();
                model.ContactTitle.Id = entity.Id;
            }
            else model.ContactTitle.Id = _contactTitleRepository.Add(new ContactTitle() { Title = model.ContactTitle.Title });

            _contactRepository.Add(model.ToEntity());
        }

        public void RemoveContact(int contactId)
        {

            if (_contactRepository.Has(contactId)) _contactRepository.Remove(contactId);
        }

        public void RemoveContact(ContactModel model)
        {
            if (_contactRepository.Has(model.Id.Value)) _contactRepository.Remove(model.Id.Value);
        }

        public void Dispose()
        {
            _contactTitleRepository.Dispose();
            _contactRepository.Dispose();
            _resumeRepository.Dispose();
        }

        public bool UpdateContact(ContactModel model)
        {
            if (!model.Id.HasValue) return false;
            if (_contactRepository.Has(model.Id.Value))
            {
                var entity = _contactRepository.Get(c=>c.Id == model.Id.Value).Include(c=>c.ContactTitle).FirstOrDefault();
                entity.Data = model.Data;
                if (!entity.ContactTitle.Title.Equals(model.ContactTitle.Title))
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
            var resume = _resumeRepository.Get(addModel.ResumeManagerId.Value);

            if (resume.Contacts.Count == 0)
            {
                this.CreateContact(new ContactModel() { Data = addModel.EMail, ContactTitle = _contactTitleRepository.Get(t => t.Title.Equals("EMail")).FirstOrDefault().ToModel(), ResumeId = resume.Id });
                this.CreateContact(new ContactModel() { Data = addModel.Phone, ContactTitle = _contactTitleRepository.Get(t => t.Title.Equals("Phone")).FirstOrDefault().ToModel(), ResumeId = resume.Id });
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

            foreach (var contact in addModel.Contacts)
            {
                contact.ResumeId = resume.Id;
                // обновить контакт если такой есть
                if (!this.UpdateContact(contact))
                    // создать новый если нет
                    this.CreateContact(contact);
            }

        }
    }
}
