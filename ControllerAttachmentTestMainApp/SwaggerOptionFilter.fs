module SwaggerOptionFilter

open System.Collections.Generic
open Swashbuckle.AspNetCore.SwaggerGen

open System

// преобразует
// prop: Option<T> -> prop: T (nullable: true)
// prop: T -> prop: T (nullable: false)
// в схеме свагера
type OptionFilter() =
    interface ISchemaFilter with
        member this.Apply(schema, context) =
            let isOption (t: Type) =
                t.IsGenericType
                && t.GetGenericTypeDefinition() = typedefof<Option<_>>

            let type' = context.Type

            if not (isOption type')
               && (schema.Properties |> Seq.isEmpty |> not) then
                let requires = ResizeArray<string>()
                for prop in type'.GetProperties() do
                    let propType = prop.PropertyType
                    let key =
                        schema.Properties.Keys
                        |> Seq.find (fun x -> x.ToLower() = prop.Name.ToLower())

                    if isOption propType then
                        let wrappedType = propType.GetGenericArguments().[0]

                        let newSchema =
                            context.SchemaGenerator.GenerateSchema(wrappedType, context.SchemaRepository)

                        newSchema.Nullable <- true
                        schema.Properties.[key] <- newSchema
                    else
                        schema.Properties.[key].Nullable <- false
                        requires.Add(key)
                schema.Required <- HashSet(requires)

