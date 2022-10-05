using System;
using Harjotus03;

string choice;

var nickname = "Teero123";
var pincode = "5656";

Console.WriteLine("Mitä haluat tehdä?");
Console.WriteLine("1 - Lisää uusi tuote\n" +
    "2 - Poista pelaaja\n" +
    "3 - Tulosta pelaajien määrä\n" +
    "4 - Muokkaa pelaajanimeä\n" +
    "5 - Lopeta sovellus");
choice = Console.ReadLine();

if (choice == "1")
{
    AddLogins("6060", "Tuomas");
}
else if (choice == "2")
{
    DeleteLogins("6060");
}
else if (choice == "3")
{
    QueringLogins();
}
else if (choice == "4")
{
    ChangeNickname("Taavo", "7000");
}
else if (choice == "5")
{
    return;
}
else
{
    Console.WriteLine("Not valid number");
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
            pincode = newPincode,
            nickname = newNickname
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
