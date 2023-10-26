using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Colecciones;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Backend.StringConexion
{
    class StringConexion
    {
        private IMongoCollection<BsonDocument> collection;
        private readonly IMongoDatabase database;

        public StringConexion()
        {
            // Configura la cadena de conexión
            string connectionString = "mongodb+srv://JeanDB:Wmckpm72@cluster0.gqxm3db.mongodb.net/?retryWrites=true&w=majority";

            // Crea un cliente de MongoDB
            MongoClient client = new MongoClient(connectionString);

            // Obtiene una referencia a la base de datos
            database = client.GetDatabase("Juego");


        }

        public IMongoDatabase GetDatabase()
        {
            return database;
        }
        
    }
}
