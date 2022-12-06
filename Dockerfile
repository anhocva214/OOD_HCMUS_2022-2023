FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 5259
WORKDIR /app
COPY financial_management_service/published/ ./
ENV ASPNETCORE_URLS=http://+:5259
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "financial_management_service.dll"]