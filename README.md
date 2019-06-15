# Mbs

[![Build Status](https://mbrane.visualstudio.com/IvanShiyan/_apis/build/status/Mbs.Api.Host.Ng%20-%20Azure%20Kubernetes%20Service%20-%20CI-clone?branchName=master)](https://mbrane.visualstudio.com/IvanShiyan/_build/latest?definitionId=11&branchName=master)

```shell
az aks browse --resource-group mbrane --name mbrane1

helm ls --all mbsapihostng
helm del --purge mbsapihostng

kubectl get deployment mbsapihostng
kubectl delete deployment mbsapihostng
```

mbrane1-ingress.yaml
```yaml
apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: mbrane1-ingress
  annotations:
    kubernetes.io/ingress.class: nginx
    nginx.ingress.kubernetes.io/rewrite-target: /
    certmanager.k8s.io/cluster-issuer: letsencrypt-prod
spec:
  tls:
  - hosts:
    - mbrane1.westeurope.cloudapp.azure.com
    secretName: tls-secret
  rules:
  - host: mbrane1.westeurope.cloudapp.azure.com
    http:
      paths:
      - path:
        backend:
          serviceName: mbsapihostng
          servicePort: 80
```

certificates.yaml
```yaml
apiVersion: certmanager.k8s.io/v1alpha1
kind: Certificate
metadata:
  name: tls-secret
spec:
  secretName: tls-secret
  dnsNames:
  - mbrane1.westeurope.cloudapp.azure.com
  acme:
    config:
    - http01:
        ingressClass: nginx
      domains:
      - mbrane1.westeurope.cloudapp.azure.com
  issuerRef:
    name: letsencrypt-prod
    kind: ClusterIssuer
```