using Harjotus03;

var nickname = "Jack";
var pincode = "0011";

if (AddLogins(pincode, nickname))
{
    Console.WriteLine("Tallennus onnistui.");
}
else
{
    Console.WriteLine("Tallennus ei onnistunut.");
}

static void QueringLogins()
{
    using (Pelitietokanta pelitietokanta = new())
    {
        Console.WriteLine("Rekisteröityneet pelaajat:");

        IQueryable<Login>? logins = pelitietokanta.Logins;

        if (logins is null)
        {
            Console.WriteLine("Yhtään pelaajaa ei ole rekisteröitynyt.");
            return;
        }

        foreach (Login login in logins)
        {
            Console.WriteLine(login.nickname);
        }
    }
}
static void QueringLoginByName(string nickname)
{
    using (Pelitietokanta pelitietokanta = new())
    {
        Console.WriteLine("Pelaajan " + nickname + " tiedot.");

        IQueryable<Login>? logins = pelitietokanta.Logins?.Where(login => login.nickname == nickname);

        if (logins is null)
        {
            Console.WriteLine("Pelaajaa ei löydy.");
            return;
        }
        foreach (Login login in logins)
        {
            Console.WriteLine($"{nickname} salasana on {login.pincode}");
        }
    }
}
static bool AddLogins(string newPincode, string newNickname)
{
    using (Pelitietokanta pelitietokanta = new())
    {
        Login login = new()
        {
            nickname = newNickname,
            pincode = newPincode
        };
        pelitietokanta.Logins?.Add(login);

        int affected = pelitietokanta.SaveChanges();
        return (affected == 1);
    }
}
static bool ChangeNickname(string newNickname, string pincode)
{
    using (Pelitietokanta pelitietokanta = new())
    {
        Login loginUpdate = pelitietokanta.Logins.Find(pincode);

        if (loginUpdate is null)
        {
            return false;
        }
        else
        {
            loginUpdate.nickname = newNickname;
            int affected = pelitietokanta.SaveChanges();
            return (affected == 1);
        }
    }
}
static int DeleteLogins(string pincode)
{
    using (Pelitietokanta pelitietokanta = new())
    {
        Login loginDelete = pelitietokanta.Logins.Find(pincode);
        if (loginDelete is null)
        {
            Console.WriteLine("Ei löydy!");
            return 0;
        }
        else
        {
            pelitietokanta.Remove(loginDelete);
            int affected = pelitietokanta.SaveChanges();
            return affected;
        }
    }
}
