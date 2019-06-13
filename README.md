# Mbs

[![Build Status](https://mbrane.visualstudio.com/IvanShiyan/_apis/build/status/Mbs.Api.Host.Ng%20-%20Azure%20Kubernetes%20Service%20-%20CI-clone?branchName=master)](https://mbrane.visualstudio.com/IvanShiyan/_build/latest?definitionId=11&branchName=master)

```shell
az aks browse --resource-group mbrane --name mbrane1

helm ls --all mbsapihostng
helm del --purge mbsapihostng

kubectl get deployment mbsapihostng
kubectl delete deployment mbsapihostng
```