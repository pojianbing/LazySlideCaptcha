#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/sdk:6.0 AS build
WORKDIR /src
COPY ["Lazy.SlideCaptcha.Demo/Lazy.SlideCaptcha.Demo.csproj", "Lazy.SlideCaptcha.Demo/"]
COPY ["Lazy.SlideCaptcha.Core/Lazy.SlideCaptcha.Core.csproj", "Lazy.SlideCaptcha.Core/"]
RUN dotnet restore "Lazy.SlideCaptcha.Demo/Lazy.SlideCaptcha.Demo.csproj"
COPY . .
WORKDIR "/src/Lazy.SlideCaptcha.Demo"
RUN dotnet build "Lazy.SlideCaptcha.Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lazy.SlideCaptcha.Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lazy.SlideCaptcha.Demo.dll"]