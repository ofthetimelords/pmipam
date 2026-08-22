#!/bin/bash

# A helper tool to allow me to edit the hosts file and then apply it to Kubernetes.
nano hosts.yml
kubectl create configmap hosts --from-file=hosts.yml -n pmipam --dry-run=client -o yaml | kubectl apply -f -
