FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine as builder  
 
ENV NET_CLI_TELEMETRY_OPTOUT 1
RUN mkdir -p /root/src/app/netcoreapp
WORKDIR /root/src/app/netcoreapp

# set up node
RUN apk add nodejs npm

# copy just the project file over
# this prevents additional extraneous restores
# and allows us to re-use the intermediate layer
# This only happens again if we change the csproj.
# This means WAY faster builds!
COPY Mbs.Api.Host.Ng/Mbs.Api.Host.Ng.csproj Mbs.Api.Host.Ng/
COPY ./NuGet.Config Mbs.Api.Host.Ng/
COPY Mbs.Api/Mbs.Api.csproj Mbs.Api/
COPY ./NuGet.Config Mbs.Api/
COPY Mbs/Mbs.csproj Mbs/
COPY ./NuGet.Config Mbs/
RUN dotnet restore ./Mbs.Api.Host.Ng/Mbs.Api.Host.Ng.csproj 

COPY Mbs.Api.Host.Ng Mbs.Api.Host.Ng/
COPY Mbs.Api Mbs.Api/
COPY Mbs Mbs/
COPY Shared Shared/
WORKDIR /root/src/app/netcoreapp/Mbs.Api.Host.Ng
RUN dotnet publish -c release -f netcoreapp3.0 -o published --self-contained false


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS runtime

RUN apk cache clean
ENV NET_CLI_TELEMETRY_OPTOUT 1
WORKDIR /root/  
COPY --from=builder /root/src/app/netcoreapp/Mbs.Api.Host.Ng/published .
ENTRYPOINT ["./Mbs.Api.Host.Ng"]
