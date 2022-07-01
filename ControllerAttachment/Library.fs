namespace ControllerAttachmentTestMainApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging


type MySpecialAttribute() =
    inherit Attribute()


[<ApiController>]
[<MySpecialAttribute>]
[<Route("[controller]")>]
type ExternController (logger : ILogger<ExternController>) =
    inherit ControllerBase()
    [<HttpGet>]
    member _.Get() =
        "Hi extern"
