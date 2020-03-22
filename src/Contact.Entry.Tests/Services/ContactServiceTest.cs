using ContactEntry.Api.Data;
using ContactEntry.Api.Model;
using ContactEntry.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace ContactEntry.Tests.Services
{
   public class ContactServiceTest
    {
        private ILiteDbContext _liteDbContext;
        private IContactService _contactService;
        private IOptions<LiteDbOptions> _config;
        public ContactServiceTest()
        {
            //global SET UP
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

            _config = Options.Create(configuration.GetSection("LiteDbOptions").Get<LiteDbOptions>());
            _liteDbContext = new LiteDbContext(_config);
            _contactService = new ContactService(_liteDbContext);
        }

        [Fact]
        public void Can_POST_Contact()
        {           
            //Arrange
            var contact = new Contact()
            {
                 Name = new Name
                 {
                      First = "Harold",
                      Middle="Francis",
                      Last = "Gilkey"
                 }, 
                  Address = new Address
                  {
                       Street= "8360 High Autumn Row",
                       City="Cannon",
                       State = "Delaware",
                       Zip="19797"
                  },
                   phone = new List<Phone> 
                   { 
                    new Phone{ Number="302-611-9148", Type= "home" },
                    new Phone{ Number="302-611-9145", Type ="work" }
                   },
                    Email = "harold.gilkey@yahoo.com"
            };

            //Act
            var newContactId = _contactService.Insert(contact);

            //Assert
            Assert.True(newContactId > 0);
        }

       
        [Fact]
        public void Make_Sure_Can_Get_Contact_By_Id()
        {
            //arrange
            int contactId = 1;
            //act
            var contact = _contactService.FindOne(contactId);
            //assert
            Assert.NotNull(contact);
            
        }
    }
}
