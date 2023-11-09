using MongoDB.Entities;

namespace ConsoleApp1
{
    public class MyTestClass : Entity, ICreatedOn, IModifiedOn
    {
        public string Name { get; set; }

        public string This { get; set; }

        public string That { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}