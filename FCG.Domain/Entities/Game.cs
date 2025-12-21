using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Domain.Entities
{
    public class Game
    {
        public Game(string title, string description, decimal price)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Price = price;
        }

        protected Game() { }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }


    }
}
