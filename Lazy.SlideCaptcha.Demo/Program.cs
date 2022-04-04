using Lazy.SlideCaptcha.Core.Resources;
using Lazy.SlideCaptcha.Core.Resources.Handler;
using Lazy.SlideCaptcha.Demo.Extensions;

var builder = WebApplication.CreateBuilder(args);

// appsetting≈‰÷√
builder.Services.AddSlideCaptcha(builder.Configuration);
//.DisableDefaultTemplates()
//.AddResourceProvider<CustomResourceProvider>()
//.AddResourceHandler<UrlResourceHandler>()
//.ReplaceValidator<BasicValidator>();

// ¥˙¬Î≈‰÷√∏≤∏«appsetting≈‰÷√
//builder.Services.AddSlideCaptcha(builder.Configuration, options =>
//{
//    options.Tolerant = 0.02f;
//    options.StoreageKeyPrefix = "slider-captcha";

//    options.Backgrounds.Add(new Resource(FileResourceHandler.TYPE, @"wwwroot/images/background/1.jpg"));
//    options.Templates.Add
//    (
//        TemplatePair.Create
//        (
//            new Resource(FileResourceHandler.TYPE, @"wwwroot/images/template/1/slider.png"),
//            new Resource(FileResourceHandler.TYPE, @"wwwroot/images/template/1/hole.png")
//        )
//    );
//});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
