namespace ECommerce.Models
{
    public class Customer : User
    {

        public List<string> Notifications { get; set; }
        public Customer() { }

        public Customer(int id, string name, string email, string password, string phoneNumber)
        {
            Email = email;
            UserId = id;
            Name = name;
            Password = password;
            PhoneNumber = phoneNumber;
            Role = "Customer";
            Notifications = [];
        }

        public override string ToString()
        {
            return $"Id: {UserId}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber},  Role: {Role}";
        }


    }
}
