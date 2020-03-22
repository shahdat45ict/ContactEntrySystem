using ContactEntry.Api.Data;
using ContactEntry.Api.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactEntry.Api.Services
{
    public class ContactService:IContactService
    {
        private const string COLLECTION_NAME = "contacts";
        private LiteDatabase _liteDb;

        public ContactService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<Contact> FindAll()
        {
            var result = _liteDb.GetCollection<Contact>(COLLECTION_NAME)
                .FindAll();
            return result;
        }

        public Contact FindOne(int id)
        {
            return _liteDb.GetCollection<Contact>(COLLECTION_NAME)
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(Contact contact)
        {
            return _liteDb.GetCollection<Contact>(COLLECTION_NAME)
                .Insert(contact);
        }

        public bool Update(Contact contact)
        {
            return _liteDb.GetCollection<Contact>(COLLECTION_NAME)
                .Update(contact);
        }

        public bool Delete(int id)
        {
            return _liteDb.GetCollection<Contact>(COLLECTION_NAME)
                .Delete(id);
        }
    }
}
