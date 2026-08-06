# 03 - Redis Distributed Cache

<div dir="rtl">

## دربارهٔ این پروژه

این پروژه به بررسی عملی کش توزیع‌شده (Distributed Cache) با استفاده از Redis و رابط `IDistributedCache` در یک Minimal API می‌پردازد. هدف اصلی، مقایسهٔ این رویکرد با کش درون‌حافظه‌ای (`IMemoryCache`) بررسی‌شده در پروژهٔ اول، و درک محدودیت‌هایی است که Redis برطرف می‌کند — به‌ویژه در معماری‌های چندنمونه‌ای (Scale-Out) که در آن‌ها کش باید بین چند instance از برنامه مشترک باشد.

## پیش‌نیاز اجرا

برای مشاهدهٔ نتایج این پروژه، وجود یک نمونهٔ در حال اجرای Redis الزامی است. ساده‌ترین روش، اجرای آن از طریق Docker است:

```bash
docker run --name redis-lab -p 6379:6379 -d redis
```

پیش از اجرای پروژه باید از طریق `docker ps` اطمینان حاصل شود که کانتینر Redis در حال اجراست؛ در غیر این صورت، اتصال `IDistributedCache` با خطا مواجه خواهد شد.

## سناریو و پیاده‌سازی

مراحل انجام‌شده در این پروژه به شرح زیر است:

- **اتصال به Redis**: پیکربندی `AddStackExchangeRedisCache` جهت اتصال به یک نمونهٔ Redis در حال اجرا (از طریق Docker) و ثبت `IDistributedCache` در Dependency Injection.
- **شبیه‌سازی پایگاه داده**: ایجاد یک کلاس `FakeDatabase` استاتیک با مجموعه‌ای از محصولات نمونه، شامل متدهای `GetSampleProducts`، `GetById` و `ChangeTitle`.
- **پیاده‌سازی الگوی Cache-Aside**: در دو endpoint (`GET /products` و `GET /products/{id}`) الگوی Cache-Aside به‌صورت کامل پیاده‌سازی شده است:
  - ابتدا بررسی وجود داده در Redis با `GetStringAsync`
  - در صورت عدم وجود (Cache Miss)، خواندن از `FakeDatabase` و ذخیرهٔ نتیجه در کش با `SetStringAsync`
  - بازگرداندن منبع پاسخ (`Cache (Hit)` یا `Database (Miss)`) و زمان پاسخ‌دهی جهت مشاهدهٔ مستقیم تأثیر کش.
- **Serialization**: از آنجا که `IDistributedCache` تنها رشته یا آرایهٔ بایت را می‌پذیرد، اشیاء با `JsonSerializer` به رشتهٔ JSON تبدیل و بازتبدیل می‌شوند.
- **Expiration**: تنظیم `SlidingExpiration` روی ۱۰ ثانیه برای هر دو کلید (`products` و `product:{id}`) از طریق `DistributedCacheEntryOptions`.
- **مدیریت Cache Invalidation**: پیاده‌سازی endpoint `PATCH /products/{id}` که پس از تغییر داده در پایگاه داده، هر دو کلید مرتبط (`product:{id}` و `products`) را با `RemoveAsync` از کش حذف می‌کند تا داده‌های بیات (Stale) بازگردانده نشوند.

## مفاهیم کلیدی

- تفاوت عملی `IDistributedCache` با `IMemoryCache`: ذخیره‌سازی خارج از حافظهٔ خودِ process، به‌گونه‌ای که بین چند نمونه از برنامه مشترک است.
- ضرورت Serialization دستی داده، برخلاف `IMemoryCache` که هر نوع object را مستقیماً می‌پذیرد.
- اهمیت Cache Invalidation هم‌زمان با تغییر داده (در endpoint PATCH) برای جلوگیری از نمایش داده‌ی قدیمی پس از بروزرسانی.
- مشاهدهٔ عملی منبع پاسخ (Cache در برابر Database) و زمان پاسخ‌دهی به‌عنوان معیاری مستقیم برای سنجش تأثیر کش.

</div>