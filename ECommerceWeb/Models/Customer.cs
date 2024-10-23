﻿namespace ECommerceWeb.Models
{
    public class Customer : User
    {

        public List<string> notifications { get; set; }
        public Customer() { }

        public Customer(int id, string name, string email, string password, string phoneNumber)
        {
            this.Email = email;
            this.UserId = id;
            this.Name = name;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
            this.Role = "Customer";
            notifications = new List<string>();
        }

        public override string ToString()
        {
            return $"Id: {UserId}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber},  Role: {Role}";
        }


    }
}
