using EntityPoste.Domain;
using EntityPoste.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace EntityPoste.Repository;

public class UserRepository(AppDbContext ctx) : IUserRepository, IAsyncDisposable, IDisposable
{
    public void Insert(string name, string email)
    {
        ctx.Users.Add(new User(email, name));
        ctx.SaveChanges();
    }

    public void Update(int id, string email)
    {
        var user = ctx.Users.Find(id);
        if (user == null) return;
        user.UpdateEmail(email); 
        ctx.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = ctx.Users.Find(id);
        if (user == null) return;
        ctx.Users.Remove(user);
        ctx.SaveChanges();
    }

    public IEnumerable<User> GetUsers()
    {
        return ctx.Users.ToList();
    }

    public IEnumerable<User> GetUsersByEmail(string email)
    {
        return ctx.Users.Where(u => u.Email.Contains(email)).Include(u=>u.Addresses);
    }
    public IEnumerable<string> GetProviders()
    {
       return ctx.Users.Select(u => u.Email.Substring(u.Email.IndexOf("@")+1)).Distinct();
    }
    

    public void Dispose()
    {
        ctx.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await ctx.DisposeAsync();
    }
}