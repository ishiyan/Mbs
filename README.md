# Mbs

[![Build Status](https://mbrane.visualstudio.com/IvanShiyan/_apis/build/status/Mbs.Api.Host.Ng%20-%20Azure%20Kubernetes%20Service%20-%20CI-clone?branchName=master)](https://mbrane.visualstudio.com/IvanShiyan/_build/latest?definitionId=11&branchName=master)


Docker images: Buster vs Alpine

|REPOSITORY    |TAG    |IMAGE ID     |CREATED     |SIZE  |
|--------------|-------|-------------|------------|------|
|buster/latest |latest |2deeaec121e0 |2 hours ago |266MB |
|alpine/latest |latest |b125eda34929 |2 hours ago |164MB |


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

## Cleanup Ingres/LetsEncrypt

```yaml
kubectl delete ingress mbrane1-ingress
kubectl delete certificates tls-secret
kubectl delete ClusterIssuer letsencrypt-prod
kubectl delete secrets tls-secret
kubectl delete secrets letsencrypt-prod
```

## Install Ingres/LetsEncrypt

### Update cert-manager

```shell
helm delete cert-manager
helm list
kubectl apply --validate=false -f https://raw.githubusercontent.com/jetstack/cert-manager/release-0.12/deploy/manifests/00-crds.yaml --namespace kube-system
kubectl label namespace kube-system certmanager.k8s.io/disable-validation=true
helm repo add jetstack https://charts.jetstack.io
helm repo update
helm install cert-manager --namespace kube-system --version v0.12.0 jetstack/cert-manager --set ingressShim.defaultIssuerName=letsencrypt --set ingressShim.defaultIssuerKind=ClusterIssuer
// motted hamster
helm list
```

### ClusterIssuer cert-manager resource

Create a `ClusterIssuer` resource. It is required by `cert-manager` to represent the Lets Encrypt certificate authority where the signed certificates will be obtained.

```yaml
apiVersion: cert-manager.io/v1alpha2
kind: ClusterIssuer
metadata:
  name: letsencrypt-prod
spec:
  acme:
    # The ACME production server URL
    server: https://acme-v02.api.letsencrypt.org/directory
    # Email address used for ACME registration
    email: ivan.shiyan@gmail.com
    # Name of a secret used to store the ACME account private key
    privateKeySecretRef:
      # Secret resource used to store the account's private key.
      name: letsencrypt-prod

    # Enable the HTTP-01 challenge provider
    # you prove ownership of a domain by ensuring that a particular
    # file is present at the domain
    solvers:
    - http01:
        ingress:
          class: nginx
```

Save to the `cluster-issuer.yaml` and run `kubectl apply -f cluster-issuer.yaml --namespace kube-system`.

Use `kubectl delete clusterissuer.cert-manager.io/letsencrypt-prod` to delete.

Use `kubectl describe clusterissuer.cert-manager.io/letsencrypt-prod` to view the state of status of the ACME account registration.

Use `kubectl delete secret letsencrypt-prod -o yaml --namespace kube-system` to view the certificate issued.

Use `kubectl get secret letsencrypt-prod -o yaml --namespace kube-system` to view the certificate issued.

### Ingress route

To make service of type ClusterIP publicly available, create a Kubernetes ingress resource.

```yaml
apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: mbrane1-ingress
  annotations:
    kubernetes.io/ingress.class: nginx
    nginx.ingress.kubernetes.io/rewrite-target: /
    cert-manager.io/cluster-issuer: letsencrypt-prod
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

Save to the `mbrane1-ingress.yaml` and run `kubectl apply -f mbrane1-ingress.yaml --namespace kube-system`.

Use `kubectl delete ingress mbrane1-ingress` to delete.

Use `kubectl describe ingress mbrane1-ingress --namespace kube-system` to view.

Use `kubectl get secret tls-secret -o yaml --namespace kube-system` to view.

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

Save to the `certificates.yaml` and run `kubectl apply -f certificates.yaml --namespace kube-system`.

