using MongoDB.Driver;
using MongoDB.Entities;

using System.Linq.Expressions;

namespace ConsoleApp1
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await DB.InitAsync("TestDB",
                MongoClientSettings.FromConnectionString("mongodb://root:example@127.0.0.1:27017?authSource=admin")
            );


            var entities = new List<MyTestClass>();
            for (int i = 0; i < 100; i++)
                entities.Add(new MyTestClass()
                {
                    Name = $"Test {i}",
                    That = $"That {i}",
                    This = $"This is {i}"
                });

            await DB.InsertAsync(entities);

            var resultset = await GetAllAsync(x => new { id = x.ID, text = x.Name + " (" + x.This + ")" });
            foreach (var result in resultset)
                Console.WriteLine($"This is result: {result.id} with {result.text}");
        }

        public static async Task<IList<TProjection>> GetAllAsync<TProjection>(Expression<Func<MyTestClass, TProjection?>> projectionExpression)
        {
            return await DB.Find<MyTestClass, TProjection>()
                .Project(projectionExpression)
                .ExecuteAsync();
        }
    }
}