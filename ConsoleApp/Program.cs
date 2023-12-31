using ConsoleApp.Models;
using ConsoleApp.Services;

List<User> users = new List<User>();
CustomerService customerService = new CustomerService();


do
{

    var list = customerService.GetCustomersFromFile(); // lägger listan här så att den uppdateras varje gång en kontakt tagits bort eller lagts till

    Console.Clear();

    Console.WriteLine("########## MENY ##########");
    Console.WriteLine("1. Lägg till en ny kontakt");
    Console.WriteLine("2. Visa befintliga kontakter");
    Console.WriteLine("3. Ta bort en kontakt");

    string UserInput = Console.ReadLine();

    switch (UserInput)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Let's add a user :)");

            Console.WriteLine("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();



            User NewContact = new()  // vi skapar en ny User för att kunna hamta informationen som anvandaren skriver in i konsolen
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address
            };
                customerService.AddCustomerToList(NewContact); // kallar funktionen AddCustomerToList() och anvander NewContact som argument
                customerService.SaveCustomersToFile(); // I samband med att vi lagger till kunden så sparar vi ned kunden
                Console.WriteLine("A new user has been added!");
            break;
        case "2":

            Console.Clear();
            Console.WriteLine("Here is all the users:");

            foreach (var user in list) // kör en foreach-loop för att loopa igenom alla kontakter och sedan Console.WriteL dem
            {
                Console.WriteLine("ID: " + user.Id + " Full Name: " + user.FirstName + " " + user.LastName + " Email: " + user.Email + "\n");
            }

            Console.Write("Enter the ID of the contact to show details: \n");
                    if (int.TryParse(Console.ReadLine(), out int contactId)) // anvandarinput parsas som en int
                    {
                        var selectedContact = list.FirstOrDefault(user => user.Id == contactId); // söker efter en kund med ett specifikt ID
                        if (selectedContact != null) // Hittar den en kontakt skrivs nedan kod ut
                        {
                            Console.WriteLine($"Detailed information for contact with ID {contactId}:");
                            Console.WriteLine($"First Name: {selectedContact.FirstName}");
                            Console.WriteLine($"Last Name: {selectedContact.LastName}");
                            Console.WriteLine($"Phone Number: {selectedContact.PhoneNumber}");
                            Console.WriteLine($"Email: {selectedContact.Email}");
                            Console.WriteLine($"Address: {selectedContact.Address}");
                        }
                        else
                        {
                            Console.WriteLine($"Contact with ID {contactId} not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid ID.");
                    }


            break;
        case "3":
            Console.Clear();
            Console.WriteLine("Remove a contact:");

            Console.Write("Enter the email of the contact to remove: ");
            string emailToRemove = Console.ReadLine(); // anvandarens input sparas i stringen emailToRemove 

            var removed = customerService.RemoveCustomerByEmail(emailToRemove); // och har lagger vi emailToRemove för att specificera vilken anvandare vi vill ta bort

            if (removed)
            {
                Console.WriteLine("Contact removed successfully.");
                customerService.SaveCustomersToFile(); // sparar filen
            }
            else
            {
                Console.WriteLine("Contact not found or couldn't be removed.");
            }
            break;
    }
    
    Console.ReadKey();
    
} while (true);