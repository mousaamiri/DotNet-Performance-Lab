# DotNet Performance Lab

<div dir="rtl">

## دربارهٔ این مخزن

این مخزن مجموعه‌ای از پروژه‌های کوچک و مستقل برای بررسی عملی مفاهیم کلیدی **کارایی (Performance)** در اکوسیستم .NET/C# است. مفاهیم پوشش‌داده‌شده شامل کش‌سازی (Caching)، Redis، فشرده‌سازی (Compression)، برنامه‌نویسی ناهمگام (Async) و سنجش کارایی (Benchmarking) می‌باشند.

هدف اصلی، پیاده‌سازی دستی هر مفهوم، اندازه‌گیری عینی تأثیر آن و مستندسازی نتایج به‌صورت قابل‌بازتولید است؛ به‌گونه‌ای که هر پروژه به‌تنهایی یک آزمایش عملیِ متمرکز بر روی یک تکنیک خاص محسوب می‌شود.

## فلسفهٔ ساختار

به‌جای ایجاد یک پروژهٔ یکپارچه و پیچیده، هر مفهوم در یک پروژهٔ مجزا با حداقل وابستگی‌ها پیاده‌سازی شده است. این رویکرد باعث می‌شود:
- تمرکز بر روی یک متغیر مستقل در هر آزمایش حفظ شود.
- فرایند یادگیری و بازتولید نتایج تسهیل گردد.
- امکان مقایسهٔ مستقیم اثر هر تکنیک بر روی کارایی فراهم آید.

## نقشهٔ راه

| # | پروژه | مفهوم |
|---|-------|-------|
| 01 | [MemoryCache.Basics](src/01-MemoryCache.Basics) | آشنایی با `IMemoryCache`، Absolute/Sliding Expiration |
| 02 | [CacheAside.Api](src/02-CacheAside.Api) | پیاده‌سازی الگوی Cache-Aside روی یک API واقعی |
| 03 | [Redis.DistributedCache](src/03-Redis.DistributedCache) | جایگزینی Memory Cache با Redis، تست روی چند instance |
| 04 | [CacheStampede.Demo](src/04-CacheStampede.Demo) | شبیه‌سازی و رفع مشکل Cache Stampede |
| 05 | [ResponseCaching.Api](src/05-ResponseCaching.Api) | Output Caching / Response Caching و هدرهای HTTP |
| 06 | Compression.Demo | فشرده‌سازی پاسخ‌ها با Gzip/Brotli |
| 07 | AsyncPerformance.Demo | مقایسه‌ی Sync-over-Async با Async درست، Task vs ValueTask |
| 08 | Benchmarks | اندازه‌گیری رسمی همه‌ی موارد بالا با BenchmarkDotNet |

هر پروژه شامل یک README مجزا با شرح زیر است:
- **مسئله** – چه چالشی مد نظر است؟
- **راه‌حل** – پیاده‌سازی چگونه انجام شده؟
- **نتایج** – اعداد و مشاهدات عملی حاصل از اجرا.

## پیش‌نیازها

- **.NET SDK** (نسخهٔ پایدار جدید)
- **Docker** (برای اجرای Redis در پروژه‌های شمارهٔ ۳ به بعد)

## وضعیت

در حال توسعه — پروژه‌ها به‌ترتیب شماره‌گذاری پیش می‌روند و اولین پروژه (`MemoryCache.Basics`) آغاز شده است.

</div>