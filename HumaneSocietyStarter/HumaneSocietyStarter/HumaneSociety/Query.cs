using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {        
        static DataClasses1DataContext db;

        static Query()
        {
            db = new DataClasses1DataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();
           
            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }
        
        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            switch (crudOperation)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    break;
                case "delete":
                    Employee deleteEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).SingleOrDefault();
                    db.Employees.DeleteOnSubmit(deleteEmployee);
                    db.SubmitChanges();
                    break;
                case "read":
                  
                   
                    break;
                case "update":
                    Employee updateEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).Single();
                    updateEmployee.FirstName = employee.FirstName;
                    updateEmployee.LastName = employee.LastName;
                    updateEmployee.EmployeeNumber = employee.EmployeeNumber;
                    updateEmployee.Email = employee.Email;
                    db.SubmitChanges();
                    break;
                default:
                    break;


            }
        }
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }
        internal static Animal GetAnimalByID(int id)
        {
           return = db.Animals.Where(a => a.AnimalId == id).FirstOrDefault();
           
        }
        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            Animal animalFromDB = db.Animals.Where(a => a.AnimalId == animalId).Single();
            foreach(KeyValuePair<int,string>entry in updates)
            {
                switch (entry.Key)
                {
                    case 1:
                        animalFromDB.CategoryId = GetCategoryId(entry.Value);
                        break;
                    case 2:
                        animalFromDB.Name = entry.Value;
                        break;
                    case 3:
                        animalFromDB.Age = int.Parse(entry.Value);
                        break;
                    case 4:
                        animalFromDB.Demeanor = entry.Value;
                        break;
                    case 5:
                        animalFromDB.KidFriendly = Convert.ToBoolean(entry.Value);
                        break;
                    case 6:
                        animalFromDB.PetFriendly = Convert.ToBoolean(entry.Value);
                        break;
                    case 7:
                        animalFromDB.Weight = int.Parse(entry.Value);
                        break;
                    case 8:
                        animalFromDB.AnimalId = int.Parse(entry.Value);
                        break;
                    default:
                        return;
                }
            }
            db.SubmitChanges();
        }

        internal static void RemoveAnimal(Animal animal)
        {
            Animal removeAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            db.Animals.DeleteOnSubmit(removeAnimal);
            db.SubmitChanges();
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            throw new NotImplementedException();
        }
         
   
        internal static int GetCategoryId(string categoryName)
        {
            Category categoryId = db.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
            return categoryId.CategoryId;
        }
        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
          
        }
        internal static int GetDietPlanId(string dietPlanName)
        {
            DietPlan dietPlanId = db.DietPlans.Where(d => d.Name == dietPlanName).FirstOrDefault();
            return dietPlanId.DietPlanId;
        }

        
        internal static void Adopt(Animal animal, Client client)
        {
            Client clientAdopting = db.Clients.Where(c => c.ClientId == client.ClientId).FirstOrDefault();
            Animal animalAdopted = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            Adoption adoption = new Adoption();
            adoption.ClientId = clientAdopting.ClientId;
            adoption.AnimalId = animalAdopted.AnimalId;
            adoption.ApprovalStatus = "pending";
            adoption.AdoptionFee = null;
            adoption.PaymentCollected = false;
            db.Adoptions.InsertOnSubmit(adoption);
            db.SubmitChanges();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "Pending");
            return pendingAdoptions;
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            if (isAdopted == true)
            {
                adoption.ApprovalStatus = "Approved";
                db.SubmitChanges();
            }
            else
            {
                adoption.ApprovalStatus = "Not approved";
                RemoveAdoption(adoption.AnimalId, adoption.ClientId);
            }
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            Adoption removeAdoption = db.Adoptions.Where(a => a.AnimalId == animalId).Single();
            db.Adoptions.DeleteOnSubmit(removeAdoption);
            db.SubmitChanges();

        }
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var shotsReceived = db.AnimalShots.Where(s => s.AnimalId == animal.AnimalId);
            return shotsReceived;
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}