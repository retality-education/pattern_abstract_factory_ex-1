using System;
using System.Collections.Generic;

// Базовый класс продукта
public abstract class Product
{
    public string PartNumber { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public virtual string GetDetails()
    {
        return $"Товар: {Name}, Номер: {PartNumber}, Цена: {Price}";
    }
}

// Класс для материнской платы
public class Motherboard : Product
{
    public string SocketType { get; set; }
    public int ProcessorCount { get; set; }
    public string RamType { get; set; }
    public string BusFrequency { get; set; }

    public override string GetDetails()
    {
        return base.GetDetails() + $", Сокет: {SocketType}, Количество процессоров: {ProcessorCount}, " +
               $"Оперативная память: {RamType}, Частота шины: {BusFrequency}";
    }
}

// Класс для процессора
public class Processor : Product
{
    public string SocketType { get; set; }
    public int CoreCount { get; set; }
    public decimal ClockSpeed { get; set; }
    public string ProcessTechnology { get; set; }

    public override string GetDetails()
    {
        return base.GetDetails() + $", Сокет: {SocketType}, Количество ядер: {CoreCount}, " +
               $"Тактовая частота: {ClockSpeed} ГГц, Техпроцесс: {ProcessTechnology}";
    }
}

// Класс для жесткого диска
public class HardDrive : Product
{
    public int Capacity { get; set; } // в ГБ
    public int RotationSpeed { get; set; } // в об/мин
    public string InterfaceType { get; set; }

    public override string GetDetails()
    {
        return base.GetDetails() + $", Объем: {Capacity} ГБ, Скорость вращения: {RotationSpeed} об/мин, " +
               $"Интерфейс: {InterfaceType}";
    }
}

// Фабричный метод для создания продуктов
public abstract class ProductFactory
{
    public abstract Product CreateProduct();
}

// Фабрика для материнских плат
public class MotherboardFactory : ProductFactory
{
    private readonly Motherboard _motherboard;

    public MotherboardFactory(Motherboard motherboard)
    {
        _motherboard = motherboard;
    }

    public override Product CreateProduct()
    {
        return _motherboard;
    }
}

// Фабрика для процессоров
public class ProcessorFactory : ProductFactory
{
    private readonly Processor _processor;

    public ProcessorFactory(Processor processor)
    {
        _processor = processor;
    }

    public override Product CreateProduct()
    {
        return _processor;
    }
}

// Фабрика для жестких дисков
public class HardDriveFactory : ProductFactory
{
    private readonly HardDrive _hardDrive;

    public HardDriveFactory(HardDrive hardDrive)
    {
        _hardDrive = hardDrive;
    }

    public override Product CreateProduct()
    {
        return _hardDrive;
    }
}

// Основной класс для работы с номенклатурой
public class Inventory
{
    private readonly List<Product> _products = new List<Product>();

    // Метод для добавления продукта в инвентаре
    public void AddProduct(ProductFactory factory)
    {
        var product = factory.CreateProduct();
        _products.Add(product);
        Console.WriteLine($"{product.GetDetails()} добавлен в инвентарь.");
    }

    // Метод для вывода полной номенклатуры комплектующих
    public void ShowInventory()
    {
        Console.WriteLine("Полная номенклатура комплектующих:");
        foreach (var product in _products)
        {
            Console.WriteLine(product.GetDetails());
        }
    }

    // Метод для получения детальной информации по товару по номеру
    public void ShowProductDetails(string partNumber)
    {
        var product = _products.Find(p => p.PartNumber == partNumber);
        if (product != null)
        {
            Console.WriteLine("Детальная информация:");
            Console.WriteLine(product.GetDetails());
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }
}

// Пример использования программы
class Program
{
    static void Main()
    {
        var inventory = new Inventory();

        var motherboard = new Motherboard
        {
            PartNumber = "MB001",
            Name = "ASUS ROG Strix",
            Price = 150.00m,
            SocketType = "AM4",
            ProcessorCount = 1,
            RamType = "DDR4",
            BusFrequency = "3200 MHz"
        };

        var processor = new Processor
        {
            PartNumber = "CPU001",
            Name = "AMD Ryzen 5",
            Price = 200.00m,
            SocketType = "AM4",
            CoreCount = 6,
            ClockSpeed = 3.6m,
            ProcessTechnology = "7 nm"
        };

        var hardDrive = new HardDrive
        {
            PartNumber = "HDD001",
            Name = "Seagate Barracuda",
            Price = 50.00m,
            Capacity = 1000,
            RotationSpeed = 7200,
            InterfaceType = "SATA"
        };

        inventory.AddProduct(new MotherboardFactory(motherboard));
        inventory.AddProduct(new ProcessorFactory(processor));
        inventory.AddProduct(new HardDriveFactory(hardDrive));

        inventory.ShowInventory();

        // Получение детальной информации по товару по номеру
        inventory.ShowProductDetails("CPU001");
    }
}