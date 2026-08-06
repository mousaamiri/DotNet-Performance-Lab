using _03_Redis.DistributedCache.Model;

namespace _03_Redis.DistributedCache.Data;

public static class FakeDatabase
{
    private static readonly List<Product> Products =
[
    new Product
    {
        Id = 1,
        Name = "لپتاپ ایسوس ROG Strix G14",
        Price = 1050.99m,
        Description = "لپتاپ گیمینگ با پردازنده اینتل نسل ۱۳ و کارت گرافیک RTX 4060، صفحه نمایش ۱۴ اینچی با نرخ نوسازی ۱۴۴ هرتز",
        StockQuantity = 15,
        Category = "لپتاپ",
        CreatedAt = new DateTime(2025, 1, 15),
        UpdatedAt = null
    },
    new Product
    {
        Id = 2,
        Name = "هدفون Bose QC45",
        Price = 299.99m,
        Description = "هدفون حذف نویز فعال با کیفیت صدای بی‌نظیر و باتری ۲۴ ساعته، مناسب برای مسافرت و کار",
        StockQuantity = 42,
        Category = "هدفون و هدست",
        CreatedAt = new DateTime(2024, 11, 20),
        UpdatedAt = new DateTime(2025, 2, 10)
    },
    new Product
    {
        Id = 3,
        Name = "مانیتور سامسونگ Odyssey G7",
        Price = 649.00m,
        Description = "مانیتور منحنی ۳۲ اینچی با نرخ نوسازی ۲۴۰ هرتز و رزولوشن 4K، مناسب برای گیمینگ حرفه‌ای",
        StockQuantity = 8,
        Category = "مانیتور",
        CreatedAt = new DateTime(2024, 9, 5),
        UpdatedAt = null
    },
    new Product
    {
        Id = 4,
        Name = "کیبورد مکانیکی لاجیتک G Pro X",
        Price = 149.50m,
        Description = "کیبورد گیمینگ با سوئیچ‌های قابل تعویض و نورپردازی RGB، طراحی جمع‌وجور و حرفه‌ای",
        StockQuantity = 27,
        Category = "کیبورد و ماوس",
        CreatedAt = new DateTime(2025, 2, 1),
        UpdatedAt = new DateTime(2025, 3, 15)
    },
    new Product
    {
        Id = 5,
        Name = "موس گیمینگ ریزر DeathAdder V3",
        Price = 89.99m,
        Description = "موس سبک با سنسور 30K DPI و وزن تنها ۵۹ گرم، مناسب برای بازی‌های رقابتی",
        StockQuantity = 35,
        Category = "کیبورد و ماوس",
        CreatedAt = new DateTime(2024, 12, 10),
        UpdatedAt = null
    },
    new Product
    {
        Id = 6,
        Name = "اسپیکر بلوتوثی JBL Flip 6",
        Price = 129.95m,
        Description = "اسپیکر ضد آب با صدای فراگیر و باتری ۱۲ ساعته، مناسب برای مهمانی‌ها و فضاهای باز",
        StockQuantity = 50,
        Category = "اسپیکر و صدا",
        CreatedAt = new DateTime(2025, 3, 1),
        UpdatedAt = null
    },
    new Product
    {
        Id = 7,
        Name = "تبلت اپل آیپد پرو ۱۲.۹ اینچ",
        Price = 1099.00m,
        Description = "تبلت با صفحه نمایش XDR و پردازنده M2، مناسب برای طراحی، ویرایش ویدئو و کارهای حرفه‌ای",
        StockQuantity = 6,
        Category = "تبلت",
        CreatedAt = new DateTime(2024, 8, 15),
        UpdatedAt = new DateTime(2025, 1, 20)
    },
    new Product
    {
        Id = 8,
        Name = "هارد اکسترنال وسترن دیجیتال ۲ ترابایت",
        Price = 79.99m,
        Description = "هارد پرتابل با سرعت USB 3.2 و محافظت در برابر ضربه، مناسب برای بکاپ‌گیری و انتقال داده",
        StockQuantity = 60,
        Category = "حافظه و ذخیره‌سازی",
        CreatedAt = new DateTime(2024, 10, 25),
        UpdatedAt = null
    },
    new Product
    {
        Id = 9,
        Name = "دوربین کانن EOS R50",
        Price = 749.00m,
        Description = "دوربین بدون آینه با قابلیت فیلم‌برداری 4K و فوکوس خودکار هوشمند، مناسب برای عکاسی و وبلاگ‌نویسی",
        StockQuantity = 12,
        Category = "دوربین و تصویربرداری",
        CreatedAt = new DateTime(2024, 7, 8),
        UpdatedAt = new DateTime(2025, 2, 28)
    },
    new Product
    {
        Id = 10,
        Name = "ساعت هوشمند اپل واچ سری ۹",
        Price = 399.99m,
        Description = "ساعت هوشمند با صفحه نمایش همیشه روشن و حسگر ضربان قلب و اکسیژن خون، مناسب برای ورزش و سلامتی",
        StockQuantity = 22,
        Category = "ساعت هوشمند",
        CreatedAt = new DateTime(2025, 1, 5),
        UpdatedAt = null
    }
];
    public static List<Product> GetSampleProducts()
    {
        return Products.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            })
            .ToList();
    }

    public static Product? GetById(int id)
    {
        var product = Products.ToList().FirstOrDefault(p => p.Id == id);
        return product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Category = product.Category,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public static bool? ChangeTitle(int id, string newTitle)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return null;
        product.UpdatedAt = DateTime.UtcNow;
        product.Name = newTitle;
        return true;
    }
}