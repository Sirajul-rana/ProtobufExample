using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using ProtobufExample.Models;

namespace ProtobufExample.Controllers
{
    public class MongoDbController : ApiController
    {
        private readonly MongoDatabase mongoDatabase;
        private MongoClient _client;
        private MongoServer _server;

        public MongoDbController()
        {
            _client = new MongoClient(new MongoUrl(ConfigurationManager.ConnectionStrings["MongoHQ"].ConnectionString));
            _server = _client.GetServer();
            mongoDatabase = _server.GetDatabase("restaurent");
        }

        public List<User> GetAll()
        {
            List<User> model = new List<User>();
            var userList = mongoDatabase.GetCollection<User>("users").FindAll();
            foreach (User aUser in userList)
            {
                User user = new User();
                user.Id = aUser.Id;
                user.Name = aUser.Name;
                user.Password = aUser.Password;

                model.Add(user);
            }
            return model;
        }
    }
}
