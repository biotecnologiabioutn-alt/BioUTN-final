using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BioUTN.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7024/api";
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Configurar cliente HttpClient general (para Login)
            builder.Services.AddHttpClient("BioUTNAPI", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl + "/");
            });

            // Autenticación por Cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(12);
                });
            
            builder.Services.AddHttpContextAccessor();

            // Servicios Propios
            builder.Services.AddScoped<BioUTN.Servicios.IBlobStorageService, BioUTN.Servicios.BlobStorageService>();

            // Configurar Crud<T>
            Crud<BloqueDisponibilidad>.EndPoint = $"{apiBaseUrl}/BloqueDisponibilidads";
            Crud<CategoriaDocumento>.EndPoint = $"{apiBaseUrl}/CategoriasDocumento";
            Crud<Documento>.EndPoint = $"{apiBaseUrl}/Documentos";
            Crud<EntradaDiario>.EndPoint = $"{apiBaseUrl}/EntradaDiarios";
            Crud<Equipo>.EndPoint = $"{apiBaseUrl}/Equipos";
            Crud<Especie>.EndPoint = $"{apiBaseUrl}/Especies";
            Crud<Familia>.EndPoint = $"{apiBaseUrl}/Familias";
            Crud<FaseCultivo>.EndPoint = $"{apiBaseUrl}/FaseCultivos";
            Crud<Genero>.EndPoint = $"{apiBaseUrl}/Generos";
            Crud<KardexMovimiento>.EndPoint = $"{apiBaseUrl}/KardexMovimientos";
            Crud<LoteCultivo>.EndPoint = $"{apiBaseUrl}/LoteCultivos";
            Crud<MedioCultivo>.EndPoint = $"{apiBaseUrl}/MedioCultivos";
            Crud<MonitoreoFitosanitario>.EndPoint = $"{apiBaseUrl}/MonitoreoFitosanitarios";
            Crud<PermisoAmbiental>.EndPoint = $"{apiBaseUrl}/PermisoAmbientals";
            Crud<PlantaMadre>.EndPoint = $"{apiBaseUrl}/PlantaMadres";
            Crud<Protocolo>.EndPoint = $"{apiBaseUrl}/Protocolos";
            Crud<Proyecto>.EndPoint = $"{apiBaseUrl}/Proyectos";
            Crud<ProyectoUsuario>.EndPoint = $"{apiBaseUrl}/ProyectoUsuarios";
            Crud<Reactivo>.EndPoint = $"{apiBaseUrl}/Reactivos";
            Crud<ReservaEquipo>.EndPoint = $"{apiBaseUrl}/ReservaEquipos";
            Crud<Rol>.EndPoint = $"{apiBaseUrl}/Rols";
            Crud<TareaOperativa>.EndPoint = $"{apiBaseUrl}/TareaOperativas";
            Crud<Taxonomia>.EndPoint = $"{apiBaseUrl}/Taxonomias";
            Crud<TipoCultivo>.EndPoint = $"{apiBaseUrl}/TipoCultivos";
            Crud<TipoIdentificacion>.EndPoint = $"{apiBaseUrl}/TipoIdentificacions";
            Crud<TipoPlanta>.EndPoint = $"{apiBaseUrl}/TipoPlantas";
            Crud<UbicacionFisica>.EndPoint = $"{apiBaseUrl}/UbicacionFisicas";
            Crud<UnidadFrasco>.EndPoint = $"{apiBaseUrl}/UnidadFrascos";
            Crud<Usuario>.EndPoint = $"{apiBaseUrl}/Usuarios";
            Crud<UnidadMedida>.EndPoint = $"{apiBaseUrl}/UnidadesMedida";
            Crud<SolicitudLab>.EndPoint = $"{apiBaseUrl}/SolicitudesLab";
            Crud<SolicitudDetalleMaterial>.EndPoint = $"{apiBaseUrl}/SolicitudesDetalleMaterial";


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Use static files middleware
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
