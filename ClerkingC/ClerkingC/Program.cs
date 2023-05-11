using System;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ClerkingC
{
    public class Invoice
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string invoice_number { get; set; }
        public ObjectId client_id { get; set; }
        public DateTime date { get; set; }
        public double total_amount { get; set; }
    }

    public class Expenses
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public ObjectId invoice_id { get; set; }
    }

    public class Clients
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string name  { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

    }

    public class Employees
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public ObjectId department_id { get; set; }

    }

    public class Departments
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public ObjectId employee_id { get; set; }

    }

    internal class Program
    {

        public static void MainMenu(MongoClient client)
        {
            string reading = "";

            while (true)
            {
                reading = Console.ReadLine();
                if (reading == "1")
                {
                    Console.WriteLine("1) Show Menu;");
                    Console.WriteLine("2) Show all collections;");
                    Console.WriteLine("3) Add object;");
                    Console.WriteLine("4) Delete object;");
                    Console.WriteLine("5) Search document;");
                    Console.WriteLine("6) Sort and show document;");
                    Console.WriteLine("Please, write a number to choose function or write stop for end");
                }
                else if (reading == "2")
                {
                    ShowCol(client);

                }
                else if (reading == "3")
                {
                    AddObjectMenu(client);
                    Console.WriteLine(reading);
                }
                else if (reading == "4")
                {
                    DeleteObjectMenu(client);
                    Console.WriteLine(reading);
                }
                else if (reading == "5")
                {
                    SearchDocument(client);
                    Console.WriteLine(reading);
                }
                else if (reading == "6")
                {
                    SortAndShowDocument(client);
                    Console.WriteLine(reading);
                }
                else if (reading == "stop")
                {
                    Console.WriteLine(reading);
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command, showing menu again");

                }
            }
        }

        public static void ShowCol(MongoClient client)
        {
            client = new MongoClient("mongodb://localhost:27018");
            var database = client.GetDatabase("accounting");
            var collectionNames = database.ListCollectionNames().ToList();
            Console.WriteLine("Collections: ");
            for (int i = 0; i < collectionNames.Count; i++)
            {
                Console.WriteLine(collectionNames[i]);
            }
        }

        public static void AddObjectMenu(MongoClient client)
        {
            string reading = "";

            Console.WriteLine("1) Add object to Invoice;");
            Console.WriteLine("2) Add object to Expense;");
            Console.WriteLine("3) Add object to Client;");
            Console.WriteLine("4) Add object to Employee;");
            Console.WriteLine("5) Add object to Department;");
            Console.WriteLine("6) Go back;");
            Console.WriteLine("Please, write a number to choose function.");

            reading = Console.ReadLine();
            if (reading == "1")
            {
                AddObjectInvoice(client);
            }
            else if (reading == "2")
            {
                AddObjectExpense(client);

            }
            else if (reading == "3")
            {
                AddObjectClient(client);

            }
            else if (reading == "4")
            {
                AddObjectEmployee(client);

            }
            else if (reading == "5")
            {
                AddObjectDepartment(client);

            }
            else if (reading == "6")
            {
                MainMenu(client);
                
            }
            else
            {
                Console.WriteLine("Unknown command");
                AddObjectMenu(client);
            }
        }

        public static void AddObjectInvoice(MongoClient client)
        {
            Console.WriteLine("Write info about: objectId, InvoiceNumber, ClientId and TotalAmount");
            string objid = "";
            string invoicenum = "";
            string clientid = "";
            int total = 0;
            while (true)
            {
                objid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ObjectId accepted");
                    break;
                }
            }

            invoicenum = Console.ReadLine();
            Console.WriteLine("InvoiceNumber accepted");

            while (true)
            {
                clientid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ClientId accepted");
                    break;
                }
            }

            total = int.Parse(Console.ReadLine());
            Console.WriteLine("Total amount accepted");

            var database = client.GetDatabase("accounting");
            var invoiceCollection = database.GetCollection<Invoice>("invoices");
            var newInvoice = new Invoice
            {
                Id = new ObjectId(objid),
                invoice_number = invoicenum,
                client_id = new ObjectId(clientid),
                date = DateTime.UtcNow,
                total_amount = total
            };
            invoiceCollection.InsertOne(newInvoice);
            Console.WriteLine("Object added");
        }


        public static void AddObjectExpense(MongoClient client)
        {
            Console.WriteLine("Write info about: objectId, Description, InvoiceId and TotalAmount");
            string objid = "";
            string descr = "";
            string invoiceid = "";
            int total = 0;
            while (true)
            {
                objid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ObjectId accepted");
                    break;
                }
            }

            descr = Console.ReadLine();
            Console.WriteLine("Description accepted");

            while (true)
            {
                invoiceid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("InvoiceId accepted");
                    break;
                }
            }

            total = int.Parse(Console.ReadLine());
            Console.WriteLine("Total amount accepted");


            var database = client.GetDatabase("accounting");
            var expenseCollection = database.GetCollection<Expenses>("expenses");
            var newExpense = new Expenses
            {
                Id = new ObjectId(objid),
                description = descr,
                amount = total,
                invoice_id = new ObjectId(invoiceid)
            };
            expenseCollection.InsertOne(newExpense);
            Console.WriteLine("Object added");
        }

        public static void AddObjectClient(MongoClient client)
        {
            var database = client.GetDatabase("accounting");

            Console.WriteLine("Write info about: objectId, Name, Address and Phone");
            string objid = "";
            string name = "";
            string adress = "";
            string phone = "";
            while (true)
            {
                objid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ObjectId accepted");
                    break;
                }
            }

            name = Console.ReadLine();
            Console.WriteLine("Name accepted");


            adress = Console.ReadLine();
            Console.WriteLine("Adress accepted");

            phone = Console.ReadLine();
            Console.WriteLine("Phone accepted");


            var clientCollection = database.GetCollection<Clients>("clients");
            var newClient = new Clients
            {

                Id = new ObjectId(objid),
                name = name,
                address = adress,
                phone = phone,
            };
            clientCollection.InsertOne(newClient);
            Console.WriteLine("Object added");
        }

        public static void AddObjectEmployee(MongoClient client)
        {
            var database = client.GetDatabase("accounting");

            Console.WriteLine("Write info about: objectId, Name and DepartmentId");
            string objid = "";
            string name = "";
            string departmentid = "";
            while (true)
            {
                objid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ObjectId accepted");
                    break;
                }
            }

            name = Console.ReadLine();
            Console.WriteLine("Name accepted");

            while (true)
            {
                departmentid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("DepartmentId accepted");
                    break;
                }
            }

            var employeeCollection = database.GetCollection<Employees>("employees");
            var newEmployee = new Employees
            {
                Id = new ObjectId(objid),
                name=name,
                department_id = new ObjectId(departmentid)

                //{ "name", "Sarah Davis" },
                //{ "department_id", new ObjectId("6095d3ba1c2b8a4e62dc697f") }

            };
            employeeCollection.InsertOne(newEmployee);
            Console.WriteLine("Object added");
        }

        public static void AddObjectDepartment(MongoClient client)
        {
            var database = client.GetDatabase("accounting");

            Console.WriteLine("Write info about: objectId, Name and EmployeeId");
            string objid = "";
            string name = "";
            string employeeid = "";
            while (true)
            {
                objid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ObjectId accepted");
                    break;
                }
            }

            name = Console.ReadLine();
            Console.WriteLine("Name accepted");

            while (true)
            {
                employeeid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("EmployeeId accepted");
                    break;
                }
            }

            var departmentCollection = database.GetCollection<Departments>("departments");
            var newDepartment = new Departments
            {
                Id=new ObjectId(objid),
                name = name,
                employee_id = new ObjectId(employeeid)
            };
            departmentCollection.InsertOne(newDepartment);
            Console.WriteLine("Object added");

        }

        public static void DeleteObjectMenu(MongoClient client)
        {
            string reading = "";

            Console.WriteLine("1) Delete object at Invoice;");
            Console.WriteLine("2) Delete object at Expense;");
            Console.WriteLine("3) Delete object at Client;");
            Console.WriteLine("4) Delete object at Employee;");
            Console.WriteLine("5) Delete object at Department;");
            Console.WriteLine("6) Go back;");
            Console.WriteLine("Please, write a number to choose function.");

            reading = Console.ReadLine();
            if (reading == "1")
            {
                DeleteObjectInvoice(client);
            }
            else if (reading == "2")
            {
                DeleteObjectExpense(client);

            }
            else if (reading == "3")
            {
                DeleteObjectClient(client);

            }
            else if (reading == "4")
            {
                DeleteObjectEmployee(client);

            }
            else if (reading == "5")
            {
                DeleteObjectDepartment(client);

            }
            else if (reading == "6")
            {
                MainMenu(client);               
            }
            else
            {
                Console.WriteLine("Unknown command");
                DeleteObjectMenu(client);
            }
        }

        public static void DeleteObjectInvoice(MongoClient client)
        {
            var database = client.GetDatabase("accounting");
            var invoiceCollection = database.GetCollection<BsonDocument>("invoices");
            var invoices = invoiceCollection.Find(new BsonDocument()).ToList();

            Console.WriteLine("List of records from the collection " + invoices + ":");
            foreach (var invoice in invoices)
            {
                var id = invoice.GetValue("_id").ToString();
                var inv = invoice.GetValue("invoice_number").ToString();
                var clientid = invoice.GetValue("client_id").ToString();
                var date = invoice.GetValue("date").ToString();
                var total = invoice.GetValue("total_amount").ToDouble();
                Console.WriteLine($"ObjectId: {id}, Invoice number: {inv}, Client id: {clientid}, Date: {date}, Total amount: {total}");
            }

            Console.WriteLine("Select the option by which the object will be deleted: ");
            Console.WriteLine("1) ObjectId");
            Console.WriteLine("2) Invoice number");
            int choose = int.Parse(Console.ReadLine());

            if (choose == 1)
            {
                Console.WriteLine("Write Id of object which you want to delete:");
                string idtoDelete = Console.ReadLine();
                var idToDelete = new ObjectId(idtoDelete);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", idToDelete);
                invoiceCollection.DeleteOne(filter);
            }
            else if (choose == 2)
            {
                Console.WriteLine("Write invoice number of object which you want to delete:");
                string invtoDelete = Console.ReadLine();
                var invoicenumToDelete = invtoDelete;
                var filter = Builders<BsonDocument>.Filter.Eq("invoice_number", invoicenumToDelete);
                invoiceCollection.DeleteOne(filter);
            }
            Console.WriteLine("Object deleted");
        }

        public static void DeleteObjectExpense(MongoClient client)
        {
            var database = client.GetDatabase("accounting");
            var expenseCollection = database.GetCollection<BsonDocument>("expenses");
            var expenses = expenseCollection.Find(new BsonDocument()).ToList();

            Console.WriteLine("List of records from the collection " + expenses + ":");
            foreach (var expense in expenses)
            {
                var id = expense.GetValue("_id").ToString();
                var description = expense.GetValue("description").AsString;
                var amount = expense.GetValue("amount").ToDouble();
                Console.WriteLine($"ObjectId: {id}, Description: {description}, Amount: {amount}");
            }

            Console.WriteLine("Select the option by which the object will be deleted: ");
            Console.WriteLine("1) ObjectId");
            Console.WriteLine("2) Description");
            int choose = int.Parse(Console.ReadLine());

            if (choose == 1)
            {
                Console.WriteLine("Write Id of object which you want to delete:");
                string idtoDelete = Console.ReadLine();
                var idToDelete = new ObjectId(idtoDelete);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", idToDelete);
                expenseCollection.DeleteOne(filter);
            }
            else if (choose == 2)
            {
                Console.WriteLine("Write description of object which you want to delete:");
                string descrtoDelete = Console.ReadLine();
                var descriptionToDelete = descrtoDelete;
                var filter = Builders<BsonDocument>.Filter.Eq("description", descriptionToDelete);
                expenseCollection.DeleteOne(filter);
            }
            Console.WriteLine("Object deleted");
        }

        public static void DeleteObjectClient(MongoClient clientss) 
        {

            var database = clientss.GetDatabase("accounting");
            var clientCollection = database.GetCollection<BsonDocument>("clients");
            var clients = clientCollection.Find(new BsonDocument()).ToList();

            Console.WriteLine("List of records from the collection " + clients + ":");
            foreach (var client in clients)
            {
                var id = client.GetValue("_id").ToString();
                var name = client.GetValue("name").ToString();
                var address = client.GetValue("address").ToString();
                var phone = client.GetValue("phone").ToString();
                Console.WriteLine($"ObjectId: {id}, Name: {name}, Address: {address}, Phone: {phone}");
            }

            Console.WriteLine("Select the option by which the object will be deleted: ");
            Console.WriteLine("1) ObjectId");
            Console.WriteLine("2) Name");
            int choose = int.Parse(Console.ReadLine());

            if (choose == 1)
            {
                Console.WriteLine("Write Id of object which you want to delete:");
                string idtoDelete = Console.ReadLine();
                var idToDelete = new ObjectId(idtoDelete);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", idToDelete);
                clientCollection.DeleteOne(filter);
            }
            else if (choose == 2)
            {
                Console.WriteLine("Write invoice number of object which you want to delete:");
                string nametoDelete = Console.ReadLine();
                var nameToDelete = nametoDelete;
                var filter = Builders<BsonDocument>.Filter.Eq("name", nameToDelete);
                clientCollection.DeleteOne(filter);
            }
            Console.WriteLine("Object deleted");
        }

        public static void DeleteObjectEmployee(MongoClient client)
        {
            var database = client.GetDatabase("accounting");
            var employeeCollection = database.GetCollection<BsonDocument>("employees");
            var employees = employeeCollection.Find(new BsonDocument()).ToList();

            Console.WriteLine("List of records from the collection " + employees + ":");
            foreach (var employee in employees)
            {
                var id = employee.GetValue("_id").ToString();
                var name = employee.GetValue("name").ToString();
                var deptid = employee.GetValue("department_id").ToString();
                Console.WriteLine($"ObjectId: {id}, Name: {name}, Department id: {deptid}");
            }

            Console.WriteLine("Select the option by which the object will be deleted: ");
            Console.WriteLine("1) ObjectId");
            Console.WriteLine("2) Name");
            int choose = int.Parse(Console.ReadLine());

            if (choose == 1)
            {
                Console.WriteLine("Write Id of object which you want to delete:");
                string idtoDelete = Console.ReadLine();
                var idToDelete = new ObjectId(idtoDelete);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", idToDelete);
                employeeCollection.DeleteOne(filter);
            }
            else if (choose == 2)
            {
                Console.WriteLine("Write invoice number of object which you want to delete:");
                string ntoDelete = Console.ReadLine();
                var nameToDelete = ntoDelete;
                var filter = Builders<BsonDocument>.Filter.Eq("name", nameToDelete);
                employeeCollection.DeleteOne(filter);
            }
            Console.WriteLine("Object deleted");
        }

        public static void DeleteObjectDepartment(MongoClient client)
        {
            var database = client.GetDatabase("accounting");
            var departmentCollection = database.GetCollection<BsonDocument>("departments");
            var departments = departmentCollection.Find(new BsonDocument()).ToList();

            Console.WriteLine("List of records from the collection " + departments + ":");
            foreach (var department in departments)
            {
                var id = department.GetValue("_id").ToString();
                var name = department.GetValue("name").ToString();
                var empid = department.GetValue("employee_id").ToString();
                Console.WriteLine($"ObjectId: {id}, Name: {name}, Employee id: {empid}");
            }

            Console.WriteLine("Select the option by which the object will be deleted: ");
            Console.WriteLine("1) ObjectId");
            Console.WriteLine("2) Name");
            int choose = int.Parse(Console.ReadLine());

            if (choose == 1)
            {
                Console.WriteLine("Write Id of object which you want to delete:");
                string idtoDelete = Console.ReadLine();
                var idToDelete = new ObjectId(idtoDelete);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", idToDelete);
                departmentCollection.DeleteOne(filter);
            }
            else if (choose == 2)
            {
                Console.WriteLine("Write invoice number of object which you want to delete:");
                string ntoDelete = Console.ReadLine();
                var nameToDelete = ntoDelete;
                var filter = Builders<BsonDocument>.Filter.Eq("name", nameToDelete);
                departmentCollection.DeleteOne(filter);
            }
            Console.WriteLine("Object deleted");
        }

        public static void SearchDocument(MongoClient client)
        {

            var database = client.GetDatabase("accounting");

            Console.WriteLine("Enter a table that may contain the desired object:");
            string table = Console.ReadLine();
            var collection = database.GetCollection<BsonDocument>(table);

            Console.WriteLine("Enter the id of the object you are looking for:");
            string objid = "";
            while (true)
            {
                objid = Console.ReadLine();
                if (objid.Length != 24)
                {
                    Console.WriteLine("Wrong Length of Id, it must be 24, write again");
                }
                else
                {
                    Console.WriteLine("ObjectId accepted");
                    break;
                }
            }

            var objectIdToFind = new ObjectId(objid);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectIdToFind);
            var foundObj = collection.Find(filter).FirstOrDefault();

            if (foundObj != null)
            {
                Console.WriteLine(foundObj.ToJson());
            }
            else
            {
                Console.WriteLine("Объект не найден");
            }
        }

        public static void SortAndShowDocument(MongoClient client)
        {
            Console.WriteLine("Choose collection to sort and show:");
            Console.WriteLine("1) Invoices;");
            Console.WriteLine("2) Expenses;");
            Console.WriteLine("3) Clients;");
            Console.WriteLine("4) Employees;");
            Console.WriteLine("5) Departments;");
            Console.WriteLine("6) Back");
            int choose = int.Parse(Console.ReadLine());
            Console.WriteLine("Ascending or Descending? 1 or 2?");
            int asc = int.Parse(Console.ReadLine());
            var database = client.GetDatabase("accounting");
            


            if (choose == 1)
            {
                var invoices = database.GetCollection<BsonDocument>("invoices");
                
                if (asc == 2)
                {
                    var sortedInvoices = invoices.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("total_amount")).ToList();                    
                    foreach (var invoice in sortedInvoices)
                    {
                        Console.WriteLine(invoice);
                    }
                }
                else
                {
                    var sortedInvoices = invoices.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Ascending("total_amount")).ToList();
                    foreach (var invoice in sortedInvoices)
                    {
                        Console.WriteLine(invoice);
                    }
                }
                Console.WriteLine("Sorted by total amount");
            }
            else if (choose == 2)
            {
                var expenses = database.GetCollection<BsonDocument>("expenses");

                if (asc == 2)
                {
                    var sortedExpenses = expenses.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("amount")).ToList();
                    foreach (var expense in sortedExpenses)
                    {
                        Console.WriteLine(expense);
                    }
                }
                else
                {
                    var sortedExpenses = expenses.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Ascending("amount")).ToList();
                    foreach (var expense in sortedExpenses)
                    {
                        Console.WriteLine(expense);
                    }
                }
                Console.WriteLine("Sorted by amount");
            }
            else if (choose == 3)
            {
                var clients = database.GetCollection<BsonDocument>("clients");

                if (asc == 2)
                {
                    var sortedclients = clients.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("name")).ToList();
                    foreach (var cli in sortedclients)
                    {
                        Console.WriteLine(cli);
                    }
                }
                else
                {
                    var sortedclients = clients.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Ascending("name")).ToList();
                    foreach (var cli in sortedclients)
                    {
                        Console.WriteLine(cli);
                    }
                }
                Console.WriteLine("Sorted by name");
            }
            else if (choose == 4)
            {
                var employees = database.GetCollection<BsonDocument>("employees");

                if (asc == 2)
                {
                    var sortedEmployees = employees.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("name")).ToList();
                    foreach (var emp in sortedEmployees)
                    {
                        Console.WriteLine(emp);
                    }
                }
                else
                {
                    var sortedEmployees = employees.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Ascending("name")).ToList();
                    foreach (var emp in sortedEmployees)
                    {
                        Console.WriteLine(emp);
                    }
                }
                Console.WriteLine("Sorted by name");
            }
            else if (choose == 5)
            {
                var departments = database.GetCollection<BsonDocument>("departments");

                if (asc == 2)
                {
                    var sortedDepartments = departments.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("name")).ToList();
                    foreach (var dep in sortedDepartments)
                    {
                        Console.WriteLine(dep);
                    }
                }
                else
                {
                    var sortedDepartments = departments.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Ascending("name")).ToList();
                    foreach (var dep in sortedDepartments)
                    {
                        Console.WriteLine(dep);
                    }
                }
                Console.WriteLine("Sorted by name");
            }
            Console.WriteLine("Returned to menu");
        }

        static  void Main(string[] args)
        {
            Console.WriteLine("Hello, this is easy Clerk prog");
            Console.WriteLine("You need to write some number to use any function");
            Console.WriteLine("Here is some functions available to use(menu):");
            
            MongoClient client = new MongoClient("mongodb://localhost:27018");
            var database = client.GetDatabase("accounting");

            Console.WriteLine("1) Show Menu;");
            Console.WriteLine("2) Show all collections;");
            Console.WriteLine("3) Add object;");
            Console.WriteLine("4) Delete object;");
            Console.WriteLine("5) Search document;");
            Console.WriteLine("6) Sort and show document;");
            Console.WriteLine("Please, write a number to choose function or write stop for end");

            MainMenu(client);
            
            Console.WriteLine("Program end its working, GoodBye");
        }
    }
}
