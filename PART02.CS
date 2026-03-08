using System;

class Ticket
{
    private static int counter = 0;
    private decimal price;

    public int TicketId { get; }
    public string MovieName { get; set; }

    public decimal Price
    {
        get { return price; }
        protected set
        {
            if (value > 0)
                price = value;
        }
    }

    public decimal PriceAfterTax
    {
        get { return Price * 1.14m; }
    }

    public Ticket(string movieName, decimal price)
    {
        counter++;
        TicketId = counter;
        MovieName = movieName;
        Price = price;
    }

    public virtual void PrintTicket()
    {
        Console.WriteLine($"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | After Tax: {PriceAfterTax:F2} EGP");
    }

  
    public void SetPrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public void SetPrice(decimal basePrice, decimal multiplier)
    {
        Price = basePrice * multiplier;
    }

    public static int GetTotalTickets()
    {
        return counter;
    }
}

class StandardTicket : Ticket
{
    public string SeatNumber { get; set; }

    public StandardTicket(string movieName, decimal price, string seat)
        : base(movieName, price)
    {
        SeatNumber = seat;
    }

    public override void PrintTicket()
    {
        base.PrintTicket();
        Console.WriteLine($"Seat: {SeatNumber}");
    }
}

class VIPTicket : Ticket
{
    public bool LoungeAccess { get; set; }
    public decimal ServiceFee { get; } = 50;

    public VIPTicket(string movieName, decimal price, bool lounge)
        : base(movieName, price)
    {
        LoungeAccess = lounge;
    }

    public override void PrintTicket()
    {
        base.PrintTicket();
        Console.WriteLine($"Lounge Access: {(LoungeAccess ? "Yes" : "No")} | Service Fee: {ServiceFee} EGP");
    }
}

class IMAXTicket : Ticket
{
    public bool Is3D { get; set; }

    public IMAXTicket(string movieName, decimal price, bool is3D)
        : base(movieName, price)
    {
        Is3D = is3D;
    }

    public override void PrintTicket()
    {
        base.PrintTicket();
        Console.WriteLine($"IMAX 3D: {(Is3D ? "Yes" : "No")}");
    }
}

sealed class Projector
{
    public void Start()
    {
        Console.WriteLine("Projector started.");
    }

    public void Stop()
    {
        Console.WriteLine("Projector stopped.");
    }
}

class Cinema
{
    private Projector projector = new Projector();
    private Ticket[] tickets = new Ticket[20];

    public void AddTicket(Ticket t)
    {
        for (int i = 0; i < tickets.Length; i++)
        {
            if (tickets[i] == null)
            {
                tickets[i] = t;
                break;
            }
        }
    }

    public void PrintAllTickets()
    {
        Console.WriteLine("\n========== All Tickets ==========\n");

        foreach (var t in tickets)
        {
            if (t != null)
                t.PrintTicket();  
        }
    }

    public void OpenCinema()
    {
        Console.WriteLine("========== Cinema Opened ==========");
        projector.Start();
    }

    public void CloseCinema()
    {
        Console.WriteLine("\n========== Cinema Closed ==========");
        projector.Stop();
    }
}

class Program
{
      public static void ProcessTicket(Ticket t)
    {
        Console.WriteLine("\nProcessing Ticket:");
        t.PrintTicket();  // Dynamic Binding
    }
    static void Main()
    {
        Cinema cinema = new Cinema();
        cinema.OpenCinema();

        StandardTicket t1 = new StandardTicket("Inception", 120, "A-5");
        VIPTicket t2 = new VIPTicket("Avengers", 200, true);
        IMAXTicket t3 = new IMAXTicket("Dune", 180, false);

        
        t1.SetPrice(150);
        t1.SetPrice(100, 1.2m); 

        cinema.AddTicket(t1);
        cinema.AddTicket(t2);
        cinema.AddTicket(t3);

        cinema.PrintAllTickets();

        ProcessTicket(t2);

        cinema.CloseCinema();
        
        Console.WriteLine("\n");
    }
}

