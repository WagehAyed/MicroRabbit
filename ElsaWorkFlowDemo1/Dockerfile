#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ElsaWorkFlowDemo1/ElsaWorkFlowDemo1.csproj", "ElsaWorkFlowDemo1/"]
RUN dotnet restore "ElsaWorkFlowDemo1/ElsaWorkFlowDemo1.csproj"
COPY . .
WORKDIR "/src/ElsaWorkFlowDemo1"
RUN dotnet build "ElsaWorkFlowDemo1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElsaWorkFlowDemo1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElsaWorkFlowDemo1.dll"]