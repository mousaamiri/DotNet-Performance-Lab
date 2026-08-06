# 02 - CacheAside.Api

<div dir="rtl">

## دربارهٔ این پروژه

این پروژه، پیاده‌سازی الگوی **Cache-Aside** (که به آن Lazy Loading نیز گفته می‌شود) به‌صورت یک Web API واقعی با Minimal API در ASP.NET Core است. برخلاف پروژهٔ اول که یک برنامهٔ کنسولی تعاملی بود، اینجا هدف، شبیه‌سازی یک سناریوی واقعی HTTP با یک منبع دادهٔ درون‌حافظه‌ای (`ProductSeeder`) و یک لایهٔ کش روی آن است.

## الگوی Cache-Aside

جریان کار به این صورت است:

1. درخواست می‌رسد.
2. ابتدا کش بررسی می‌شود.
3. **Cache Hit**: داده مستقیماً از کش برگردانده می‌شود.
4. **Cache Miss**: داده از منبع اصلی (در اینجا `ProductSeeder`) خوانده می‌شود، در کش نوشته می‌شود، سپس برگردانده می‌شود.

در این پیاده‌سازی این منطق با `IMemoryCache.GetOrCreate` به‌صورت خلاصه انجام شده است:

```csharp
orders.MapGet("/{id:int}", (int id) =>
{
    var product = cache.GetOrCreate($"Product:{id}", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
        return ProductSeeder.GetSampleProducts().FirstOrDefault(p => p.Id == id);
    });
    return product;
});
```

## چالش کلیدی: Cache Invalidation

سؤال اصلی این پروژه این بود: اگر بین دو درخواست، رکورد دیتابیس آپدیت شود، کش چه مشکلی پیدا می‌کند؟

### مرحلهٔ اول — باگ پنهان (Shared Reference)

در نسخهٔ اولیه، `ProductSeeder.GetSampleProducts()` مستقیماً همان لیست استاتیک و همان instance های `Product` را برمی‌گرداند. در نتیجه، تغییر دادن یک Product (مثلاً از طریق PATCH) به‌طور خودکار و ناخواسته روی نسخهٔ داخل کش هم اثر می‌گذاشت — چون هر دو به یک آبجکت در حافظه اشاره داشتند. این باعث می‌شد به نظر برسد "مشکلی نیست"، در حالی که در عمل هیچ invalidation واقعی رخ نداده بود و صرفاً یک side effect ناخواسته بود.

### مرحلهٔ دوم — رفع با Deep Copy

با تغییر `GetSampleProducts()` و `GetById()` به بازگرداندن یک **کپی جدید** از هر `Product` (به‌جای reference مستقیم)، کش به یک snapshot مستقل تبدیل شد. از این نقطه به بعد، تغییر داده در منبع اصلی دیگر به‌صورت خودکار روی کش اثر نمی‌گذاشت، و **invalidation صریح** لازم شد:

```csharp
orders.MapPatch("/{id:int}", (int id) =>
{
    var success = ProductSeeder.ChangeTitle(id, "Changed");
    if (!success) return Results.NotFound();
    cache.Remove($"Product:{id}");
    return Results.Ok($"Product {id} changed");
});
```

### نتیجه

بدون invalidation دستی، کاربر بعدی که همان endpoint را صدا بزند، دادهٔ کهنه (Stale Data) دریافت می‌کند — تا زمانی که `AbsoluteExpirationRelativeToNow` (اینجا ۱۰ دقیقه) به پایان برسد. این دقیقاً همان مشکلی است که در پروژهٔ بعدی (Cache Stampede) عمیق‌تر بررسی می‌شود: وقتی همزمان تعداد زیادی درخواست به یک کلید expire‌شده برسند.

## مفاهیم کلیدی

- تمایز بین Cache Hit و Cache Miss و مدیریت هرکدام.
- اهمیت جداسازی state کش از state منبع اصلی داده (Deep Copy در برابر Shared Reference).
- ضرورت Invalidation صریح پس از عملیات نوشتن (Write) برای جلوگیری از Stale Data.
- محدودیت‌های Minimal API در مدیریت DI و خطرات ساخت Service Provider جداگانه.
</div>