#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/Identity/Identity.csproj", "Web/Identity/"]
COPY ["FS.Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "FS.Identity/Identity.Infrastructure/"]
COPY ["FS.SharedKernel/SH.Infrastructure/SH.Infrastructure.csproj", "FS.SharedKernel/SH.Infrastructure/"]
COPY ["FS.SharedKernel/SH.Application/SH.Application.csproj", "FS.SharedKernel/SH.Application/"]
COPY ["FS.SharedKernel/SH.Domain/SH.Domain.csproj", "FS.SharedKernel/SH.Domain/"]
COPY ["FS.Identity/Identity.Role/Role.Application/Role.Application.csproj", "FS.Identity/Identity.Role/Role.Application/"]
COPY ["FS.Identity/Identity.Role/Role.Domain/Role.Domain.csproj", "FS.Identity/Identity.Role/Role.Domain/"]
COPY ["FS.Identity/Identity.User/User.Application/User.Application.csproj", "FS.Identity/Identity.User/User.Application/"]
COPY ["FS.Identity/Identity.User/User.Domain/User.Domain.csproj", "FS.Identity/Identity.User/User.Domain/"]
RUN dotnet restore "Web/Identity/Identity.csproj"
COPY . .
WORKDIR "/src/Web/Identity"
RUN dotnet build "Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.dll"]