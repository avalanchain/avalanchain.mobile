using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace avalanchain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Avatar { get; set; }
        public Currency NativeCurrency { get; set; }

        public User(string name, string avatar)
        {
            Id = Guid.NewGuid();
            Name = name;
            Avatar = avatar;
            NativeCurrency = Currency.GBP;
        }
    }
}

