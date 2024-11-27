namespace EntityPoste.Domain;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }

    public string Country { get; set; }

// Chiave esterna per la relazione con User
    public int UserId { get; set; }

// Proprietà di navigazione inversa
    public User User { get; set; }

    public override string ToString()
    {
        return $"{Street}, {City}, {Country}";
    }
}