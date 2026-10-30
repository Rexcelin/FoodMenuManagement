
using FoodMenuManagement.Models;
using FoodMenuManagement.Services;
using FoodMenuManagement.Helper;

namespace FoodMenuManagement.Menus;

public class FoodMenu
{
    private readonly FoodItemService _service;
    private readonly AuditLogService _auditLogService;

    public FoodMenu(
    FoodItemService service,
    AuditLogService auditLogService)
    {
        _service = service;
        _auditLogService = auditLogService;
    }

    private async Task FilterFoodItemsAsync()
    {
        Console.Write("Enter category (leave blank for all): ");
        string? category = Console.ReadLine();

        Console.Write("Show only available items? (y/n): ");
        string? choice = Console.ReadLine();

        bool availableOnly = choice?.Trim().Equals(
            "y",
            StringComparison.OrdinalIgnoreCase) == true;

        var foodItems = await _service.GetFilteredFoodItemsAsync(category,availableOnly);

        if (foodItems.Count == 0)
        {
            Console.WriteLine("No matching food items found.");
            return;
        }

        Console.WriteLine("\nFiltered Food Items:");

        foreach (var item in foodItems)
        {
            DisplayFoodItem(item);
        }
    }
    public async Task RunAsync()
    {

        while (true)
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("     FOOD MENU MANAGEMENT SYSTEM");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Add Food Item");
            Console.WriteLine("2. View All Food Items");
            Console.WriteLine("3. View Food Item By ID");
            Console.WriteLine("4. Update Food Item");
            Console.WriteLine("5. Delete Food Item");
            Console.WriteLine("6. Filter Food Items");
            Console.WriteLine("7. View Audit Logs");
            Console.WriteLine("8. Exit");
            Console.WriteLine("==================================");

            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await AddFoodItemAsync();
                        break;

                    case "2":
                        await ViewAllFoodItemsAsync();
                        break;

                    case "3":
                        await ViewFoodItemByIdAsync();
                        break;

                    case "4":
                        await UpdateFoodItemAsync();
                        break;

                    case "5":
                        await DeleteFoodItemAsync();
                        break;

                    case "6":
                        await FilterFoodItemsAsync();
                        break;

                    case "7":
                        await ViewAuditLogsAsync();
                        break;

                    case "8":
                        Console.WriteLine("Thank you for using Food Menu Management!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DatabaseErrorHandler.GetErrorMessage(ex));
            }

            Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
        }
    }

    private async Task AddFoodItemAsync()
    {
        Console.WriteLine("\n--- ADD FOOD ITEM ---");

        Console.Write("Enter food name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter category: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Enter description (optional): ");
        string? description = Console.ReadLine();

        Console.Write("Enter price: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        Console.Write("Is available? (y/n): ");
        bool isAvailable =
            (Console.ReadLine() ?? "").Trim().Equals(
                "y", StringComparison.OrdinalIgnoreCase);

        var foodItem = new FoodItem
        {
            Name = name,
            Category = category,
            Description = string.IsNullOrWhiteSpace(description)
                ? null
                : description,
            Price = price,
            IsAvailable = isAvailable
        };

        int id = await _service.AddFoodItemAsync(foodItem);

        Console.WriteLine($"Food item added successfully. ID: {id}");
    }

    private async Task ViewAllFoodItemsAsync()
    {
        Console.WriteLine("\n--- ALL FOOD ITEMS ---");

        var items = await _service.GetAllFoodItemsAsync();

        if (items.Count == 0)
        {
            Console.WriteLine("No food items found.");
            return;
        }

        foreach (var item in items)
        {
            DisplayFoodItem(item);
        }
    }

    private async Task ViewFoodItemByIdAsync()
    {
        Console.WriteLine("\n--- SEARCH FOOD ITEM ---");

        Console.Write("Enter food item ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var item = await _service.GetFoodItemByIdAsync(id);

        if (item == null)
        {
            Console.WriteLine("Food item not found.");
            return;
        }

        DisplayFoodItem(item);
    }

    private async Task UpdateFoodItemAsync()
    {
        Console.WriteLine("\n--- UPDATE FOOD ITEM ---");

        Console.Write("Enter food item ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var item = await _service.GetFoodItemByIdAsync(id);

        if (item == null)
        {
            Console.WriteLine("Food item not found.");
            return;
        }

        Console.Write($"Enter new name ({item.Name}): ");
        string? name = Console.ReadLine();

        Console.Write($"Enter new category ({item.Category}): ");
        string? category = Console.ReadLine();

        Console.Write($"Enter new description ({item.Description}): ");
        string? description = Console.ReadLine();

        Console.Write($"Enter new price ({item.Price}): ");
        string? priceInput = Console.ReadLine();

        Console.Write($"Available? (y/n, current: {item.IsAvailable}): ");
        string? availableInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(name))
            item.Name = name;

        if (!string.IsNullOrWhiteSpace(category))
            item.Category = category;

        if (description != null)
            item.Description = string.IsNullOrWhiteSpace(description)
                ? null
                : description;

        if (!string.IsNullOrWhiteSpace(priceInput))
        {
            if (!decimal.TryParse(priceInput, out decimal newPrice))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            item.Price = newPrice;
        }

        if (!string.IsNullOrWhiteSpace(availableInput))
        {
            if (availableInput.Equals("y", StringComparison.OrdinalIgnoreCase))
                item.IsAvailable = true;
            else if (availableInput.Equals("n", StringComparison.OrdinalIgnoreCase))
                item.IsAvailable = false;
            else
            {
                Console.WriteLine("Enter y or n for availability.");
                return;
            }
        }

        bool updated = await _service.UpdateFoodItemAsync(item);

        Console.WriteLine(
            updated ? "Food item updated successfully."
                    : "Food item could not be updated.");
    }

    private async Task DeleteFoodItemAsync()
    {
        Console.WriteLine("\n--- DELETE FOOD ITEM ---");

        Console.Write("Enter food item ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var item = await _service.GetFoodItemByIdAsync(id);

        if (item == null)
        {
            Console.WriteLine("Food item not found.");
            return;
        }

        Console.Write($"Delete {item.Name}? (y/n): ");

        string confirmation = Console.ReadLine() ?? "";

        if (!confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Delete cancelled.");
            return;
        }

        bool deleted = await _service.DeleteFoodItemAsync(id);

        Console.WriteLine(
            deleted ? "Food item deleted successfully."
                    : "Food item could not be deleted.");
    }


    private static void DisplayFoodItem(FoodItem item)
    {
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"ID          : {item.Id}");
        Console.WriteLine($"Name        : {item.Name}");
        Console.WriteLine($"Category    : {item.Category}");
        Console.WriteLine($"Description : {item.Description ?? "N/A"}");
        Console.WriteLine($"Price       : {item.Price:F2}");
        Console.WriteLine($"Available   : {(item.IsAvailable ? "Yes" : "No")}");
        Console.WriteLine("----------------------------------");
    }

    private async Task ViewAuditLogsAsync()
    {
        Console.WriteLine("\n========== AUDIT LOG HISTORY ==========");

        var logs = await _auditLogService.GetAllAuditLogsAsync();

        if (logs.Count == 0)
        {
            Console.WriteLine("No audit records found.");
            return;
        }

        foreach (var log in logs)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Audit ID    : {log.AuditLogId}");
            Console.WriteLine($"Entity      : {log.EntityName}");
            Console.WriteLine($"Entity ID   : {log.EntityId}");
            Console.WriteLine($"Action      : {log.Action}");
            Console.WriteLine($"Details     : {log.Details}");
            Console.WriteLine($"Old Values  : {log.OldValues ?? "N/A"}");
            Console.WriteLine($"New Values  : {log.NewValues ?? "N/A"}");
            Console.WriteLine($"Performed At: {log.PerformedAt:yyyy-MM-dd HH:mm:ss} UTC");
            Console.WriteLine("---------------------------------------");
        }
    }
}
