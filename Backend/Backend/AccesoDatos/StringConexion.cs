using Backend.Colecciones;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.AccesoDatos
{
     class StringConexion
    {
        private readonly IMongoDatabase database;

        public StringConexion()
        {
            string connectionString = "mongodb+srv://JeanDB:Wmckpm72@cluster0.gqxm3db.mongodb.net/?retryWrites=true&w=majority";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("Juego");
        }

        public IMongoDatabase GetDatabase()
        {
            return database;
        }

       
    }

}
