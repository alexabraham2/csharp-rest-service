# Introduction

csharp-rest-service provides you an out-of-the-box example of a .NET WebAPI application with a PostgreSQL backend.

The application also includes several Steeltoe management endpoints (including a database health check) and dynamic log level management.

For more details on Steeltoe, visit <https://docs.steeltoe.io/>.

## Running the app locally

To run the sample application:

```script
dotnet run
```

## Database

You will need a local database running, see [DATABASE.md](DATABASE.md#local).

## Deploying to Kubernetes as a TAP workload with Tanzu CLI

If you make modifications to the source, push these changes to your own Git repository.

Deploy your app from your Git repository by running:

```script
tanzu apps workload apply -f config/workload.yaml
```

Deploy your app from your local working directory by running:

```script
tanzu apps workload create csharp-rest-service -f config/workload.yaml --local-path . --type web
```

## Accessing the app deployed to your cluster

Determine the URL to use for the accessing the app by running:

```script
tanzu apps workload get csharp-rest-service
```

To access the deployed app, use the URL shown under "Workload Knative Services", appending the endpoint `/api/customer-profiles`.

This depends on the TAP installation having DNS configured for the Knative ingress.

## Inner loop experience with the app deployed to your Cluster

You may use TAP's Visual Studio plugin to enable inner loop development features including Live Update and Remote Debug.

You will have to update some fields in the root directory's `Tiltfile` to connect your live session to Kubernetes.

Update the `allow_k8s_contexts` line of the `Tiltfile` to indicate the Kubernetes context to use.

```script
export K8S_TEST_CONTEXT="a-kubernetes-context"
tilt up
tilt down
```


# Spring Boot PostgreSQL Fragment

## Using the fragment

To include this fragment you should add an import to the `accelerator` section:

```yaml
accelerator:
  # ...
  imports:
  - name: spring-boot-postgresql
    
```

Then in your `engine` section add an `InvokeFragment` directive at an appropriate level.

```yaml
engine:
  # ...
    - type: InvokeFragment
      reference: spring-boot-postgresql
      let:
        - name: databaseName
          expression: "#databaseName"
        - name: databaseIntegrationTestType
          expression: "'testcontainers'"
```

## Creating the fragment resource

To create this fragment use:

```yaml
apiVersion: accelerator.apps.tanzu.vmware.com/v1alpha1
kind: Fragment
metadata:
  name: spring-boot-postgresql
  namespace: accelerator-system
spec:
  displayName: "Spring Boot PostgreSQL"
  git:
    ref:
      branch: main
    url: https://github.com/vmware-tanzu/application-accelerator-samples.git
    subPath: fragments/spring-boot-postgresql
```
