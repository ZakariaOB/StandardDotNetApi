FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY StandardApi.csproj /build/

RUN dotnet restore ./build/StandardApi.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./StandardApi.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0.0
WORKDIR /app
COPY --from=build /build/out .
ENV ConnectionStrings__DefaultConnection="Server=db-server;Database=StandardApi;User=sa;Password=ZakariaBoukhris@1987!"
ENTRYPOINT ["dotnet", "StandardApi.dll"]