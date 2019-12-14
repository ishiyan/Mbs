FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder  
 
ENV NET_CLI_TELEMETRY_OPTOUT 1
RUN mkdir -p /root/src/app/netcoreapp
WORKDIR /root/src/app/netcoreapp

# set up node
ENV NODE_VERSION 10.9.0
ENV NODE_DOWNLOAD_SHA d061760884e4705adfc858eb669c44eb66cd57e8cdf6d5d57a190e76723af416
RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

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
RUN dotnet publish -c release -f netcoreapp3.1 -o published --self-contained false


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

ENV NET_CLI_TELEMETRY_OPTOUT 1
WORKDIR /root/  
COPY --from=builder /root/src/app/netcoreapp/Mbs.Api.Host.Ng/published .
ENTRYPOINT ["./Mbs.Api.Host.Ng"]
