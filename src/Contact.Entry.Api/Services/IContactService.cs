using ContactEntry.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactEntry.Api.Services
{
   public interface IContactService
    {       
        IEnumerable<Contact> FindAll();
        Contact FindOne(int id);
        int Insert(Contact contact);
        bool Update(Contact contact);
        bool Delete(int id);
    }
}
