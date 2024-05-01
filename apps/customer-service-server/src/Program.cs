using System.Reflection;
using CustomerServiceManagement.APIs;
using CustomerServiceManagement.APIs.Graphql;
using CustomerServiceManagement.Infrastructure;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IServiceRequestsService, ServiceRequestsService>();
builder.Services.AddScoped<IServiceTicketsService, ServiceTicketsService>();
builder.Services.AddScoped<IFeedbacksService, FeedbacksService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

// Add graphql services to the container.
builder.Services.AddSingleton<ISchema, GqlSchema>(services => new GqlSchema(
    new SelfActivatingServiceProvider(services)
));
builder.Services.AddSingleton(typeof(AutoRegisteringInputObjectGraphType<>));
builder.Services.AddSingleton(typeof(AutoRegisteringObjectGraphType<>));
builder.Services.AddSingleton(typeof(EnumerationGraphType<>));

builder.Services.AddGraphQL(b =>
    b.AddSystemTextJson()
        .AddDataLoader()
        .AddAutoSchema<GqlSchema>()
        .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)
);

// Add a DbContext to the container
builder.Services.AddDbContext<CustomerServiceManagementContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DbContext"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(builder =>
{
    builder.AddPolicy(
        "MyCorsPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(["localhost", "https://studio.apollographql.com"])
                .AllowCredentials();
        }
    );
});

var app = builder.Build();

app.UseCors();
app.MapGraphQL("/graphql").RequireCors("MyCorsPolicy");
app.MapGraphQLPlayground("/graphql/ui");
app.MapGraphQLVoyager();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
