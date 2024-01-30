using System;

class RepositoryAdminSystem
{
    private static int SectionValue = GetSectionValue(); 
    private static int[,] Consumers;
    private static int[,] Stocks;
    private static int[,] Acquisitions;

    static void Main(string[] args)
    {
        int[] initialFillValue = GenerateInitialFillValue();
        int groupCustomerId = initialFillValue[0];
        int groupStockId = initialFillValue[1];
        int groupAquiId = initialFillValue[2];
        InitializeArrays(groupCustomerId, groupStockId, groupAquiId);
        ShowWelcomeScreen();
        while (true)
        {
            ShowMainMenu();
        }
    }
private static int[] GenerateInitialFillValue()
{
    Console.WriteLine("Enter the number of group members:");
    int memberCount = Convert.ToInt32(Console.ReadLine());
    string concatenatedFirstDigits = "";
    string concatenatedSecondDigits = "";
    string concatenatedThirdDigits = "";

    for (int i = 0; i < memberCount; i++)
    {
        Console.WriteLine($"Enter ID for member {i + 1}:");
        string id = Console.ReadLine();
        if (id.Length > 2) // Make sure ID is long enough
        {
            concatenatedFirstDigits += id[0];
            concatenatedSecondDigits += id[1];
            concatenatedThirdDigits += id[2];
        }
        else
        {
            Console.WriteLine("ID must be at least 3 digits long.");
            i--; // Decrement i to retry entering ID for the same member
        }
    }

    int generatedIdConsumer = -Convert.ToInt32(concatenatedFirstDigits);
    int generatedIdStock = -Convert.ToInt32(concatenatedSecondDigits);
    int generatedIdAqui = -Convert.ToInt32(concatenatedThirdDigits);
    int[] generatedIds = new int[] { generatedIdConsumer, generatedIdStock, generatedIdAqui };

    Console.WriteLine($"IDs generated for Consumers, Stocks, Acquisitions to fill the arrays initially: {generatedIdConsumer}, {generatedIdStock}, {generatedIdAqui}");
    return generatedIds;
}


private static void InitializeArrays(int groupCustomerId, int groupStockId, int groupAquiId)
    {
        Consumers = new int[50 * SectionValue, 2];
        Stocks = new int[50 * SectionValue, 3];
        Acquisitions = new int[50 * SectionValue, 3];

        FillArray(Consumers, groupCustomerId);
        FillArray(Stocks, groupStockId);
        FillArray(Acquisitions, groupAquiId);
    }
    private static int GetSectionValue()
    {
    Console.WriteLine("Enter your section (e.g., 'M1' or 'F1'):");
    string section = Console.ReadLine().Trim().ToUpper();

    switch (section)
    {
        case "M1":
            return 1;
        case "F1":
            return 2;
        // Add more cases as needed for different sections
        default:
            Console.WriteLine("Invalid section. Defaulting to M1.");
            return 1;
    }
    }


    private static void FillArray(int[,] array, int fillValue)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = fillValue;
            }
        }
    }

    private static void ShowWelcomeScreen()
    {
        Console.WriteLine("Welcome to the Repository Administration System");
        // Add your group's names and IDs here
    }

    private static void ShowMainMenu()
{
    while (true)
    {
        Console.WriteLine("\nMain Menu:");
        Console.WriteLine("A. Stocks");
        Console.WriteLine("B. Consumers");
        Console.WriteLine("C. Acquisitions");
        Console.WriteLine("D. Exit");
        Console.Write("Enter your choice: ");

        string input = Console.ReadLine();
        char choice = input.Length > 0 ? char.ToUpper(input[0]) : ' '; // Taking only the first character

        switch (choice)
        {
            case 'A':
                ManageStocks();
                break;
            case 'B':
                ManageConsumers();
                break;
            case 'C':
                ManageAcquisitions();
                break;
            case 'D':
                Console.WriteLine("Exited Successfully!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}


  private static void ManageStocks()
{
    while (true)
    {
        Console.WriteLine("\nStock Management:");
        Console.WriteLine("1. Add a new stock");
        Console.WriteLine("2. Update stock price");
        Console.WriteLine("3. Update stock quantity");
        Console.WriteLine("4. List all stocks");
        Console.WriteLine("5. Record a sale"); // New option for recording a sale
        Console.WriteLine("0. Return to Main Menu");
        Console.Write("Enter your choice: ");

        string input = Console.ReadLine();
        char choice = input.Length > 0 ? input[0] : ' '; // Taking only the first character

        switch (choice)
        {
            case '1':
                AddNewStock();
                break;
            case '2':
                UpdateStockPrice();
                break;
            case '3':
                UpdateStockQuantity();
                break;
            case '4':
                ListAllStocks();
                break;
            case '0':
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

private static void AddNewStock()
{
    Console.WriteLine("\nEnter Stock ID:");
    int stockId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Stock Price:");
    int price = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Quantity:");
    int quantity = Convert.ToInt32(Console.ReadLine());

    // Check if stock ID already exists
    for (int i = 0; i < Stocks.GetLength(0); i++)
    {
        if (Stocks[i, 0] == stockId)
        {
            Console.WriteLine("Stock ID already exists. Please try again.");
            return;
        }
    }

    // Add new stock
    for (int i = 0; i < Stocks.GetLength(0); i++)
    {
        if (Stocks[i, 0] < 0) // Assuming negative values mean unused
        {
            Stocks[i, 0] = stockId;
            Stocks[i, 1] = price;
            Stocks[i, 2] = quantity;
            Console.WriteLine("Stock added successfully.");
            return;
        }
    }

    Console.WriteLine("No space to add new stock.");
}

private static void UpdateStockPrice()
{
    Console.WriteLine("\nEnter Stock ID to update price:");
    int stockId = Convert.ToInt32(Console.ReadLine());

    bool found = false;

    for (int i = 0; i < Stocks.GetLength(0); i++)
    {
        if (Stocks[i, 0] == stockId)
        {
            Console.WriteLine("Enter new price:");
            int newPrice = Convert.ToInt32(Console.ReadLine());

            Stocks[i, 1] = newPrice;
            Console.WriteLine("Stock price updated successfully.");
            found = true;
            break;
        }
    }

    if (!found)
    {
        Console.WriteLine("Stock ID not found.");
    }
}


private static void UpdateStockQuantity()
{
    Console.WriteLine("\nEnter Stock ID to update quantity:");
    int stockId = Convert.ToInt32(Console.ReadLine());

    bool found = false;

    for (int i = 0; i < Stocks.GetLength(0); i++)
    {
        if (Stocks[i, 0] == stockId)
        {
            Console.WriteLine("Enter new quantity:");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            Stocks[i, 2] = newQuantity;
            Console.WriteLine("Stock quantity updated successfully.");
            found = true;
            break;
        }
    }

    if (!found)
    {
        Console.WriteLine("Stock ID not found.");
    }
}


private static void ListAllStocks()
    {
        Console.WriteLine("\nListing all stocks:");
        for (int i = 0; i < Stocks.GetLength(0); i++)
        {
            if (Stocks[i, 0] >= 0) // Listing only used records
            {
                Console.WriteLine($"Stock ID: {Stocks[i, 0]}, Price: {Stocks[i, 1]}, Quantity: {Stocks[i, 2]}");
            }
        }
    }


   private static void ManageConsumers()
{
    while (true)
    {
        Console.WriteLine("\nConsumer Management:");
        Console.WriteLine("1. Add a new consumer");
        Console.WriteLine("2. Update consumer phone number");
        Console.WriteLine("3. Search by phone number");
        Console.WriteLine("0. Return to Main Menu");
        Console.Write("Enter your choice: ");

        string input = Console.ReadLine();
        char choice = input.Length > 0 ? input[0] : ' '; // Taking only the first character

        switch (choice)
        {
            case '1':
                AddNewConsumer();
                break;
            case '2':
                UpdateConsumerPhoneNumber();
                break;
            case '3':
                SearchByPhoneNumber();
                break;
            case '0':
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}


private static void AddNewConsumer()
{
    Console.WriteLine("\nEnter Consumer ID:");
    int consumerId = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter Phone Number (up to 8 digits):");
    string phoneNumberInput = Console.ReadLine();
    
    // Convert to integer only if the input is valid
    if (!int.TryParse(phoneNumberInput, out int phoneNumber) || phoneNumberInput.Length > 8)
    {
        Console.WriteLine("Invalid phone number. Please enter up to 8 digits.");
        return; // Exit if the phone number is not valid
    }

    // Check if consumer ID already exists
    for (int i = 0; i < Consumers.GetLength(0); i++)
    {
        if (Consumers[i, 0] == consumerId)
        {
            Console.WriteLine("Consumer ID already exists. Please try again.");
            return;
        }
    }

    // Add new consumer
    for (int i = 0; i < Consumers.GetLength(0); i++)
    {
        if (Consumers[i, 0] < 0) // Assuming negative values mean unused
        {
            Consumers[i, 0] = consumerId;
            Consumers[i, 1] = phoneNumber;
            Console.WriteLine("Consumer added successfully.");
            return;
        }
    }

    Console.WriteLine("No space to add new consumer.");
}



private static void UpdateConsumerPhoneNumber()
{
    Console.WriteLine("\nEnter Consumer ID to update phone number:");
    int consumerId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter new phone number:");
    int newPhoneNumber = Convert.ToInt32(Console.ReadLine());

    for (int i = 0; i < Consumers.GetLength(0); i++)
    {
        if (Consumers[i, 0] == consumerId)
        {
            Consumers[i, 1] = newPhoneNumber;
            Console.WriteLine("Phone number updated successfully.");
            return;
        }
    }

    Console.WriteLine("Consumer ID not found.");
}

private static void SearchByPhoneNumber()
{
    Console.WriteLine("\nEnter Phone Number to search:");
    int phoneNumber = Convert.ToInt32(Console.ReadLine());

    bool found = false;  // Flag to check if a match is found

    for (int i = 0; i < Consumers.GetLength(0); i++)
    {
        if (Consumers[i, 1] == phoneNumber)
        {
            Console.WriteLine($"Consumer ID: {Consumers[i, 0]}");
            Console.WriteLine($"Phone Number: {Consumers[i, 1]}");
            found = true;
            break;  // Exit the loop once a match is found
        }
    }

    if (!found)
    {
        Console.WriteLine("Phone number not found.");
    }
}




   private static void ManageAcquisitions()
{
    while (true)
    {
        Console.WriteLine("\nAcquisition Management:");
        Console.WriteLine("1. New Acquisition");
        Console.WriteLine("2. Delete Acquisition");
        Console.WriteLine("0. Return to Main Menu");
        Console.Write("Enter your choice: ");

        string input = Console.ReadLine();
        char choice = input.Length > 0 ? input[0] : ' '; // Taking only the first character

        switch (choice)
        {
            case '1':
                AddNewAcquisition();
                break;
            case '2':
                DeleteAcquisition();
                break;
            case '0':
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}


private static void AddNewAcquisition()
{
    int acquisitionId = GenerateAcquisitionId();
    Console.WriteLine("Generated Acquisition ID: " + acquisitionId); // Display the generated Acquisition ID

    Console.WriteLine("\nEnter Consumer ID:");
    int consumerId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Stock ID:");
    int stockId = Convert.ToInt32(Console.ReadLine());

    // Add new acquisition
    for (int i = 0; i < Acquisitions.GetLength(0); i++)
    {
        if (Acquisitions[i, 0] < 0) // Assuming negative values mean unused
        {
            Acquisitions[i, 0] = acquisitionId;
            Acquisitions[i, 1] = consumerId;
            Acquisitions[i, 2] = stockId;
            Console.WriteLine("Acquisition added successfully.");
            return;
        }
    }

    Console.WriteLine("No space to add new acquisition.");
}


private static int GenerateAcquisitionId()
{
    int maxId = -1;
    for (int i = 0; i < Acquisitions.GetLength(0); i++)
    {
        if (Acquisitions[i, 0] > maxId)
        {
            maxId = Acquisitions[i, 0];
        }
    }
    return maxId + 1;
}

private static void DeleteAcquisition()
{
    Console.WriteLine("\nEnter Acquisition ID:");
    int acquisitionId = Convert.ToInt32(Console.ReadLine());

    bool found = false;

    for (int i = 0; i < Acquisitions.GetLength(0); i++)
    {
        if (Acquisitions[i, 0] == acquisitionId)
        {
            Acquisitions[i, 0] = Acquisitions[i, 1] = Acquisitions[i, 2] = -1;  // Marking as deleted
            Console.WriteLine("Acquisition deleted successfully.");
            found = true;
            break;
        }
    }

    if (!found)
    {
        Console.WriteLine("Acquisition ID not found.");
    }
}



}
