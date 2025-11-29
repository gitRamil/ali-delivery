#!/bin/bash

set -e  # Остановить выполнение при любой ошибке

echo "Applying base configurations..."
kubectl apply -f ./base/namespace.yaml
kubectl apply -f ./base/configmap.yaml
kubectl apply -f ./base/secrets.yaml
kubectl apply -f ./configs/app-config.yaml

echo "Setting up ingress controller..."
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml
kubectl delete validatingwebhookconfigurations ingress-nginx-admission

echo "Applying networking and infrastructure..."
kubectl apply -f ./networking/ingress.yaml
kubectl apply -f ./infrastructure/postgres.yaml
kubectl apply -f ./infrastructure/rabbitmq.yaml
kubectl apply -f ./infrastructure/seq.yaml

echo "Building and deploying order service..."
docker build -t order-service:1.0 -f "../../order/Ali.Delivery.Order.WebApi/Dockerfile" "../../order"
kubectl apply -f ./services/order-service.yaml

echo "Starting port forwarding..."
kubectl port-forward -n ed-namespace service/order-service 8080:80