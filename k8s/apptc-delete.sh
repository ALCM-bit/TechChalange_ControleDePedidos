#!/bin/bash

# Delete configmap
kubectl delete -f apptc-configmap.yaml

# Delete deployment
kubectl delete -f apptc-deployment.yaml

# Delete service
kubectl delete -f apptc-svc.yaml

# Delete hpa
kubectl delete -f apptc-hpa.yaml

echo "Configurações deletadas!"
