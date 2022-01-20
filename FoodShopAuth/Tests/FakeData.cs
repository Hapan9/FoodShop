using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DAL.Models;

namespace Tests
{
    internal class FakeData
    {
        public FakeData()
        {
            Fixture = new Fixture();

            Users = Fixture.CreateMany<User>(10)
                .ToList();
        }

        public List<User> Users { get; }

        public Fixture Fixture { get; }
    }
}