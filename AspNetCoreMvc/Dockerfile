FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app/
COPY *.csproj .
RUN dotnet restore 
COPY . .
RUN dotnet publish AspNetCoreMvc.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS="http://*:4500"
ENTRYPOINT ["dotnet","AspNetCoreMvc.dll"]   