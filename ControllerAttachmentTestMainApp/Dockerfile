#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ControllerAttachmentTestMainApp/ControllerAttachmentTestMainApp.fsproj", "ControllerAttachmentTestMainApp/"]
RUN dotnet restore "ControllerAttachmentTestMainApp/ControllerAttachmentTestMainApp.fsproj"
COPY . .
WORKDIR "/src/ControllerAttachmentTestMainApp"
RUN dotnet build "ControllerAttachmentTestMainApp.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControllerAttachmentTestMainApp.fsproj" -c Release -o /app/publish
#-----
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ControllerAttachment/ControllerAttachment.fsproj", "ControllerAttachment/"]
RUN dotnet restore "ControllerAttachment/ControllerAttachment.fsproj"
COPY . .
WORKDIR "/src/ControllerAttachment"
RUN dotnet build "ControllerAttachment.fsproj" -c Release -o /app/build


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ControllerAttachmentTestMainApp.dll"]