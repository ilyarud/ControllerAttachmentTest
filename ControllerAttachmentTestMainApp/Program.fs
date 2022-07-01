namespace ControllerAttachmentTestMainApp

open Microsoft.OpenApi.Models
open System.Reflection

#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open System.Runtime.InteropServices
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open SwaggerOptionFilter
open ControllerAttachmentTestMainApp.Controllers

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)
        // Пдключаем 
        //let path = Path.Combine(Environment.CurrentDirectory, "ControllerAttachment.dll")
        //let asm = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
        //builder.Services.AddControllers().AddApplicationPart(asm)
        // 
        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen
            (fun x ->
                x.SwaggerDoc("v1", OpenApiInfo(Title = "ReportsService", Version = "v1"))
                x.SchemaFilter<OptionFilter>()) |> ignore

        let app = builder.Build()

        app.UseSwagger() |> ignore

        app.UseSwaggerUI(fun c -> c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportService.Api v1"))
        |> ignore

        app.MapControllers()

        app.Run()

        exitCode
