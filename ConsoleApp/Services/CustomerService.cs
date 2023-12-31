using ConsoleApp.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ConsoleApp.Services;

public interface ICustomerService // Interfacet CustomerService dar 4 olika metoder lagras
{
    bool AddCustomerToList(User customer); // för att lagga till anvandare,
    IEnumerable<User> GetCustomersFromFile(); // Hamta anvandare och omformatera dem till .NET
    bool RemoveCustomerByEmail(string email); // Ta bort en kontakt genom att ange e-post
    void SaveCustomersToFile(); // Spara ned anvandaren till listan
}

// \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

public class CustomerService : ICustomerService
{
    private readonly FileService _fileService = new FileService(@"C:\PROGRAMMERING\C#\Demo\content.json");
    private List<User> _userList = new();


    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    public bool AddCustomerToList(User customer) // Metod för att lagga till en anvandare
    {
        try
        {
            if (_userList.Any(c => c.Email == customer.Email)) // Så lange det inte finns en anvandare med samma e-post så laggar vi till den i listan _userList som en "customer"
            {
                throw new InvalidOperationException("User already exists");
            }
            else

                _userList.Add(customer);

            for (var i = 0; i < _userList.Count; i++) // ger varje anvandare ett ID, första får 1, andra 2 osv...
            {
                var userId = customer.Id = i + 1;
            }

            _fileService.SaveContentToFile(JsonConvert.SerializeObject(_userList));

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    public IEnumerable<User> GetCustomersFromFile() // hamtar uppdaterad lista av anvandare
    {
        try
        {
            var content = _fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content)) 
            {
                _userList = JsonConvert.DeserializeObject<List<User>>(content)!;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return _userList;
    }

    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    public void SaveCustomersToFile() // spara ned en anvandare
    {
        _fileService.SaveContentToFile(JsonConvert.SerializeObject(_userList));
    }

    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    public bool RemoveCustomerByEmail(string email) // ta bort en anvandare genom att ange anvandarens e-postadress
    {
        try
        {
            var list = GetCustomersFromFile().ToList(); // Visa existerande anvandare
            var userToRemove = list.FirstOrDefault(i => i.Email == email);

            if (userToRemove != null)
            {
                list.Remove(userToRemove); // ta bort anvandaren
                _userList = list; // Uppdatera listan
                return true;
            }

            return false; // Ifall anvandaren inte hittas
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

}

