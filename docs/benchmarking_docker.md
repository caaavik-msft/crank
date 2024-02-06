## Description

This guide shows how to run a Docker job and benchmark it.

## Prerequisites

1. You should have followed the [Getting Started](getting_started.md) tutorial, and have `crank` and `crank-agent` tools available.
2. The sample requires an agent running on a **Linux** machine with Docker installed, or at least on Windows with either Docker for Windows or WSL. 

## Define the scenario

The following content is available at https://github.com/dotnet/crank/blob/main/samples/netty/netty.benchmarks.yml

It has a scenario called **netty** comprised of an _application_ job based on a Docker file and a _load_ job using **wrk** as the load generator.

```yml
imports:
  - https://raw.githubusercontent.com/dotnet/crank/main/src/Microsoft.Crank.Jobs.Wrk/wrk.yml

variables:
  localEndpoint: http://localhost:5010

jobs:
  server:
    sources:
      techempower:
        repository: https://github.com/TechEmpower/FrameworkBenchmarks
        branchOrCommit: master
    dockerFile: techempower/frameworks/Java/netty/netty.dockerfile
    dockerImageName: netty
    dockerContextDirectory: techempower/frameworks/Java/netty
    port: 8080

scenarios:
  netty:
    application:
      job: server
    load:
      job: wrk
      variables:
        serverPort: 8080
        path: /plaintext

profiles:
  local:
    variables:
      serverAddress: localhost
    jobs: 
      application:
        endpoints: 
          - "{{ localEndpoint }}"
      load:
        endpoints: 
          - "{{ localEndpoint }}"

```

## Run the scenario

The command line to run this scenario is the following:

```
> crank --config /crank/samples/netty/netty.benchmarks.yml --scenario netty --profile local
```

By default it will assume the agent is listening on `http://localhost:5010`. In case the agent is setup on a different machine, add the following argument:

```
--variable localEndpoint=http:[LINUX_IP]:5010
```

## Understanding the Docker configuration

The `jobs` section in the configuration file defines a job named `server`. It's `sources` property contains the information about where to download any sources necessary for the job. In this case there is a single source named `techempower` and so the source will be copied into a directory called `techempower` in the job's working directory. The `dockerFile`, `dockerImageName`, `dockerContextDirectory` are then used to define what docker command is run.

In this case it is a **Netty** application taken from the [TechEmpower](https://github.com/TechEmpower/FrameworkBenchmarks) repository which contains several types of benchmarks for any language and framework. Netty is a Java web application framework.   

```yml
  server:
    sources:
      techempower:
        repository: https://github.com/TechEmpower/FrameworkBenchmarks
        branchOrCommit: master
    dockerFile: techempower/frameworks/Java/netty/netty.dockerfile
    dockerImageName: netty
    dockerContextDirectory: techempower/frameworks/Java/netty
    port: 8080
```

The `dockerFile` property is relative to the job's working directory, and points to the Docker file to build. Note that the path starts with `techempower` because each source is copied to a directory with the name of the source.

The `dockerImageName` is used on the agent to name the container. By setting it Docker will be able to reuse previous cached containers and start benchmarks faster.

The `dockerContextDirectory` is a path representing the working directory inside the Docker file script. Any path using `./` in the Docker file will be this value. It's relative to the job's working directory.

The property `port` is used to let the Crank agent detect when the application is ready to accept requests. When specified, the agent will try to ping the expected endpoint until it successfully answers. Another option is to define the property `readyStateText` with some value that is written on the standard output once the application has started. In the case of a Netty application, the service emits `Httpd started. Listening on: 0.0.0.0/0.0.0.0:8080` when the service is ready. Similarly, an ASP.NET application outputs `Application started. Press Ctrl+C to shut down.`
