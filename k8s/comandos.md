# Configurações k8s
**Disclaimer**: Para utilizar a aplicação localmente via kubernetes, é preciso subir um container docker com a imagem do mongoDB e passar a url do banco no arquivo `k8s/apptc-configmap.yaml`.

> Antes de rodar os comandos abaixo, garanta que está na pasta `k8s`

### Executar o arquivo de métricas
```
kubectl apply -f metrics.yaml
```

### Executar configurações da aplicação
```bash
bash apptc-apply.sh
```

### Expor a porta do service no host (local)
```
minikube tunnel
```

### Verificar o provisionamento dos pods
```
kubectl get pods -w
```
ou
```
kubectl get deployments -w
```

### Comando para executar o teste de carga (K6)
```
k6 run loadTest.js
```