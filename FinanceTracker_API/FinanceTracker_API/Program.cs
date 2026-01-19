namespace FinanceTracker_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // 1. Database Connection
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Identity Setup
            builder.Services.AddIdentity<User, IdentityRole<Guid>>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // 3. JWT Authentication Setup
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // 4. Custom Services (DI)
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // 5. Swagger Configuration with JWT Support
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Finance Tracker API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http, 
                    Scheme = "Bearer", 
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Put your token only"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme 
                        {
                            Reference = new OpenApiReference 
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" 
                            }
                        },new string[] {}}});
            });

            builder.Services.AddCors(options => {
                options.AddPolicy("FreePolicy", policy => {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin(); // ?? ????? ??????? ????? ???? ????????? ??
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("FreePolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
