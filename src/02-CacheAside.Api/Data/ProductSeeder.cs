namespace _02_CacheAside.Api.Data;

using _02_CacheAside.Api.Models;

using System;
using System.Collections.Generic;

public static class ProductSeeder
{
    private static readonly List<Product> Products = [new Product
            {
                Id = 1,
                Name = "لپ‌تاپ ایسوس ROG Zephyrus G14",
                Description =
                    "لپ‌تاپ گیمینگ با پردازنده Ryzen 9 و کارت گرافیک RTX 4060، صفحه نمایش ۱۴ اینچی با نرخ بروزرسانی ۱۲۰ هرتز",
                Price = 45000000,
                StockQuantity = 15,
                Category = "لپ‌تاپ",
                CreatedAt = new DateTime(2025, 1, 15),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 2,
                Name = "هدفون بی‌سیم سونی WH-1000XM5",
                Description = "هدفون حذف نویز فعال با کیفیت صدای فوق‌العاده، باتری ۳۰ ساعته و شارژ سریع",
                Price = 7800000,
                StockQuantity = 42,
                Category = "لوازم جانبی صوتی",
                CreatedAt = new DateTime(2025, 1, 20),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 3,
                Name = "مانیتور سامسونگ Odyssey G7",
                Description = "مانیتور خمیده ۳۲ اینچی با رزولوشن 4K، نرخ بروزرسانی ۲۴۰ هرتز و زمان پاسخ ۱ میلی‌ثانیه",
                Price = 32000000,
                StockQuantity = 8,
                Category = "مانیتور",
                CreatedAt = new DateTime(2025, 1, 25),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 4,
                Name = "موبایل اپل آیفون ۱۵ پرو مکس",
                Description = "گوشی هوشمند با تراشه A17 Pro، دوربین سه‌گانه ۴۸ مگاپیکسلی و باتری ۴۴۲۲ میلی‌آمپر",
                Price = 65000000,
                StockQuantity = 23,
                Category = "موبایل",
                CreatedAt = new DateTime(2025, 2, 1),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 5,
                Name = "کیبورد مکانیکی لاجیتک G915",
                Description = "کیبورد بی‌سیم مکانیکی با کلیدهای کم‌پروفایل، نورپردازی RGB و باتری ۳۰ ساعته",
                Price = 5800000,
                StockQuantity = 31,
                Category = "لوازم جانبی کامپیوتر",
                CreatedAt = new DateTime(2025, 2, 5),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 6,
                Name = "هدفون گیمینگ ریزر Kraken V3 Pro",
                Description = "هدفون بی‌سیم با قابلیت لرزش HyperSense، میکروفون نویزگیر و باتری ۴۰ ساعته",
                Price = 6200000,
                StockQuantity = 19,
                Category = "لوازم جانبی صوتی",
                CreatedAt = new DateTime(2025, 2, 10),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 7,
                Name = "تبلت سامسونگ گلکسی تب S9",
                Description = "تبلت ۱۲.۴ اینچی با پردازنده Snapdragon 8 Gen 2، قلم S Pen و باتری ۱۰۰۹۰ میلی‌آمپر",
                Price = 28000000,
                StockQuantity = 12,
                Category = "تبلت",
                CreatedAt = new DateTime(2025, 2, 15),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 8,
                Name = "موس گیمینگ رزر Viper Ultimate",
                Description = "موس بی‌سیم فوق‌سبک با سنسور ۲۰۰۰۰ DPI، وزن ۷۴ گرم و عمر باتری ۷۰ ساعت",
                Price = 3200000,
                StockQuantity = 45,
                Category = "لوازم جانبی کامپیوتر",
                CreatedAt = new DateTime(2025, 2, 20),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 9,
                Name = "کتاب زبان برنامه‌نویسی C# 12",
                Description = "کتاب جامع آموزش #C با رویکرد پروژه‌محور، شامل مفاهیم پیشرفته و بهترین روش‌ها",
                Price = 680000,
                StockQuantity = 67,
                Category = "کتاب",
                CreatedAt = new DateTime(2025, 3, 1),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 10,
                Name = "دوربین مداربسته داهوا ۴ مگاپیکسلی",
                Description = "دوربین IP با کیفیت 4 مگاپیکسل، دید در شب ۳۰ متری و قابلیت تشخیص حرکت",
                Price = 4500000,
                StockQuantity = 28,
                Category = "امنیتی",
                CreatedAt = new DateTime(2025, 3, 5),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 11,
                Name = "کارت گرافیک ایسوس RTX 4070 Ti",
                Description = "کارت گرافیک با ۱۲ گیگابایت حافظه GDDR6X، پشتیبانی از DLSS 3 و Ray Tracing نسل چهارم",
                Price = 55000000,
                StockQuantity = 5,
                Category = "قطعات کامپیوتر",
                CreatedAt = new DateTime(2025, 3, 10),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 12,
                Name = "اسپیکر بلوتوث جی‌بی‌ال Charge 5",
                Description = "اسپیکر ضدآب با توان ۴۰ وات، باتری ۲۰ ساعته و قابلیت اتصال دو دستگاه همزمان",
                Price = 3200000,
                StockQuantity = 53,
                Category = "لوازم جانبی صوتی",
                CreatedAt = new DateTime(2025, 3, 15),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 13,
                Name = "کیف کمری جدید پیکودیزاین ۵ لیتری",
                Description = "کیف کمری سبک با محفظه‌های متعدد، جنس نایلون ضدآب و زیپ‌های محکم",
                Price = 850000,
                StockQuantity = 34,
                Category = "کیف و کوله",
                CreatedAt = new DateTime(2025, 3, 20),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 14,
                Name = "ساعت هوشمند اپل واچ سری ۹",
                Description = "ساعت هوشمند با صفحه نمایش همیشه روشن، سنسور ضربان قلب، اندازه‌گیری اکسیژن خون و GPS",
                Price = 38000000,
                StockQuantity = 9,
                Category = "پوشیدنی",
                CreatedAt = new DateTime(2025, 4, 1),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 15,
                Name = "مودم 5G هواوی 5G CPE Pro 3",
                Description = "مودم بی‌سیم با پشتیبانی از شبکه 5G و 4G، پورت LAN و قابلیت اتصال تا ۳۲ دستگاه",
                Price = 12500000,
                StockQuantity = 16,
                Category = "شبکه",
                CreatedAt = new DateTime(2025, 4, 5),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 16,
                Name = "پاوربانک کمپانی ۲۰۰۰۰ میلی‌آمپر",
                Description = "پاوربانک پرظرفیت با شارژ سریع ۶۵ وات، نمایشگر LED و دو پورت USB-C",
                Price = 2200000,
                StockQuantity = 71,
                Category = "لوازم جانبی موبایل",
                CreatedAt = new DateTime(2025, 4, 10),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 17,
                Name = "صندلی گیمینگ DXRacer Formula",
                Description = "صندلی ارگونومیک با قابلیت تنظیم ارتفاع و تکیه‌گاه، جنس چرم مصنوعی و کوسن‌های نرم",
                Price = 15500000,
                StockQuantity = 6,
                Category = "مبلمان اداری",
                CreatedAt = new DateTime(2025, 4, 15),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 18,
                Name = "هدست واقعیت مجازی متا کوئست ۳",
                Description = "هدست VR مستقل با رزولوشن 4K، پردازنده Snapdragon XR2 Gen 2 و حافظه ۱۲۸ گیگابایت",
                Price = 42000000,
                StockQuantity = 3,
                Category = "گیمینگ",
                CreatedAt = new DateTime(2025, 4, 20),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 19,
                Name = "کابل HDMI 2.1 الگانت ۲ متری",
                Description = "کابل با پشتیبانی از رزولوشن 8K، نرخ بروزرسانی ۱۲۰ هرتز و فناوری HDR پویا",
                Price = 350000,
                StockQuantity = 89,
                Category = "کابل و اتصالات",
                CreatedAt = new DateTime(2025, 5, 1),
                UpdatedAt = null,
                IsActive = true
            },

            new Product
            {
                Id = 20,
                Name = "دوربین عکاسی سونی Alpha 7 IV",
                Description = "دوربین بدون آینه فول فریم با سنسور ۳۳ مگاپیکسل، فیلمبرداری 4K و فوکوس خودکار پیشرفته",
                Price = 98000000,
                StockQuantity = 2,
                Category = "دوربین",
                CreatedAt = new DateTime(2025, 5, 5),
                UpdatedAt = null,
                IsActive = true
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
                UpdatedAt = product.UpdatedAt,
                IsActive = product.IsActive
            })
            .ToList();
    }

    public static Product? GetById(int id)
    {
        var product = Products.ToList().FirstOrDefault(p => p.Id == id);
        return product is null? null : new Product{
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Category = product.Category,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            IsActive = product.IsActive
        };
    }

    public static bool ChangeTitle(int id, string newTitle)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return false;
        product.UpdatedAt = DateTime.UtcNow;
        product.Name = newTitle;
        return true;
    }
}
