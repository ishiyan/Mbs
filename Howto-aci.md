# Create Azure Container Instance

Create a `deploy-aci.yaml` file containing the following

```yaml
apiVersion: 2018-10-01
location: westeurope
name: mbs-aci
properties:
  containers:
  - name: mbs-latest
    properties:
      image: mbrane.azurecr.io/ishiyan/mbs:latest
      resources:
        requests:
          cpu: 1
          memoryInGb: 1
      ports:
      - port: 80
  osType: Linux
  ipAddress:
    type: Public
    ports:
    - protocol: tcp
      port: '80'
tags: null
type: Microsoft.ContainerInstance/containerGroups
```

Deploy using the following command:

```shell
az container create --resource-group mbrane --file deploy-aci.yaml
```

View deployment state:

```shell
az container show --resource-group mbrane -name mbs-aci --output table
```

View container logs:

```shell
az container logs --resource-group mbrane -name mbs-aci --container-name mbs-aci
```
