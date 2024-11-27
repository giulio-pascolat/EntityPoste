using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace EntityPoste.Domain;

public record User(string Name, string Email, int Id = 0, ICollection<Address>? Addresses = default)
{
    public User(string Name, string Email, int Id) : this(Name, Email, Id, []) { }

    public string Name { get; private set; } = Name;
    public string Email { get; private set; } = Email;

    public ICollection<Address> Addresses = Addresses ?? [];

    public void UpdateEmail(string Email)
    {
        if(!IsValidEmail(Email))
            throw new ArgumentException("Email is invalid");
        this.Email = Email;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var mail = new MailAddress(email);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}