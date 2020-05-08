# Microsoft.Crank

This is the benchmarking infrastructure used by the .NET team to run benchmarks including (but not limited to) scenarios from the [TechEmpower Web Framework Benchmarks](https://www.techempower.com/benchmarks/).

One of the goals of this system is to give developers a tool that lets them work on performance and measure potential improvements very easily. Some of the capabilities are:
- Run specific .NET versions, for instance to validate a regression between two versions.
- Run docker files or docker images. That allows to benchmark any application or use any tool as a client.
- Use local builds and send any custom file with a Job. Typically you work on a feature and you want to know its impact before committing your work. 
- Collect performance traces remotely.
- Send custom environment variables. For instance to enable custom GC modes, feature flags, or debugging traces.
- Fetch any result files from the service to your local machine. 
- Save results locally as baselines, then compare runs to get copy-pastable results comparing two runs.
- Run pre-configured scenarios such than multiple developers can use the same standard ones.
- Run applications hosted on github, even on personal branches.
- Run a scenario N times to reduce noise, saves a single average result
- Run scenarios for a long period of time to perform reliability testing.
- Limit server resources using cgroups or docker container limits.
- Store results in JSON or in SQL Server for charting.

The CLI can also run Benchmarks.NET and other applications then fetch their output locally.

## Components

The benchmarking infrastructure is made of these components:
- [benchmarks-agent](src/Microsoft.Crank.Agent) - An application that executes jobs that run as part of a benchmarked.
- [benchmarks](src/Microsoft.Crank.Controller) - An application that can enqueue jobs and display the results locally (or store them in a database).

There are also some built in jobs:
- [wrk](src/Microsoft.Crank.Jobs.Wrk) - An http client benchmarking tool. This tool is used when benchmarking TechEmpower https://github.com/wg/wrk.
- [wrk2](src/Microsoft.Crank.Jobs.Wrk2) - An http client benchmarking tool optmized for latency testing https://github.com/giltene/wrk2.
- [bombardier](Microsoft.Crank.Jobs.Bombardier) - A go based http client benchmarking tool https://github.com/codesenberg/bombardier.

## Get Started

See the [Documentation](docs)

## How to Engage, Contribute, and Give Feedback

TBD

## Code of conduct

See [CODE-OF-CONDUCT](./CODE-OF-CONDUCT.md)
