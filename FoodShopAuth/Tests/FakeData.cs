using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using DAL.Models;

namespace Tests
{
    class FakeData
    {
        public List<User> Users { get; }

        public Fixture Fixture { get; }

        public FakeData()
        {
            Fixture = new Fixture();

            Users = Fixture.CreateMany<User>(10)
                .ToList();
        }
    }
}
