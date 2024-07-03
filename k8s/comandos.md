# Configurações k8s

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