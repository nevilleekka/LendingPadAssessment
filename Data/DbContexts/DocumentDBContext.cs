using BusinessEntities;
using Data.Connections;
using Data.DbContexts.Base;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;

namespace Data.DbContexts;

public class DocumentDbContext : iDbContext
{

    private IDocumentStore DocumentStore { get; set; }

    public DocumentDbContext(RavenDBConnection dbConnection)
    {
        ConnectToDatabase(dbConnection);
    }

    public void ConnectToDatabase(RavenDBConnection dBConnection)
    {

        string serverUrl = dBConnection.serverURL;


        var documentStore = new DocumentStore
        {
            Urls = new[] { dBConnection.serverURL },
            Database = dBConnection.databaseName,

            Conventions = {
                               FindCollectionName = (type)=>{
                               if (typeof(User).IsAssignableFrom(type)){return "User"; }
                               if (typeof(Branch).IsAssignableFrom(type)){return "Branch"; }
                               return DocumentConventions.DefaultGetCollectionName(type);
                               }
                          }

        };

        DocumentStore = documentStore.Initialize();
    }

    public T Create<T>(T entity) where T : class
    {
        using (var session = DocumentStore.OpenSession())
        {

            session.Store(entity);
            session.SaveChanges();
            return entity;
        }
    }

    public T Update<T>(T entity) where T : class
    {
        using (var session = DocumentStore.OpenSession())
        {
            session.Store(entity);
            session.SaveChanges();
            return entity;
        }
    }

    public int Count<T>() where T : class
    {
        using (var session = DocumentStore.OpenSession())
        {
            int count = session.Query<T>().Count();
            return count;
        }
    }

    public IEnumerable<T> RetrieveAll<T>() where T : class
    {

        using (var session = DocumentStore.OpenSession())
        {
            List<T> entities = session.Query<T>().ToList();
            return entities;
        }
    }



    public IEnumerable<T> Retrieve<T>(Func<IDocumentSession, IRavenQueryable<T>> query) where T : class
    {

        using (var session = DocumentStore.OpenSession())
        {
            IDocumentQuery<T> documentQuery = query(session).ToDocumentQuery();
            IRavenQueryable<T> finalQuery = documentQuery.ToQueryable();
            List<T> entities = documentQuery.ToList();
            return entities;
        }
    }

    public IEnumerable<T> Retrieve<T>(IRavenQueryable<T> query) where T : class
    {

        using (var session = DocumentStore.OpenSession())
        {
            //IRavenQueryable<T> query = session.Query<T>();//    .Where(e => e.Name.Contains("Syrup") || e.Name.Contains("Lager"));
            IDocumentQuery<T> documentQuery = query.ToDocumentQuery();
            IRavenQueryable<T> finalQuery = documentQuery.ToQueryable();
            List<T> entities = documentQuery.ToList();
            return entities;
        }
    }

    public void Delete<T>(T entity)
    {
        using (var session = DocumentStore.OpenSession())
        {
            session.Delete<T>(entity);
        }
    }


    public void DeleteAll<T>()
    {
        var deleteByQueryOp = new DeleteByQueryOperation($"from {typeof(T).Name}");
        var operation = DocumentStore.Operations.Send(deleteByQueryOp);
    }
}

//    Serialization = new NewtonsoftJsonSerializationConventions
//    {
//       // JsonContractResolver=new CustomJsonContractResolver(),

//        //CustomizeJsonDeserializer = deserializer =>
//        //{
//        //    deserializer = customSerializer;
//        //},
//        CustomizeJsonSerializer  = serializer =>
//        {
//            serializer.Converters.Add(new GuidToJsonConverter());
//        },
//    }