# Sample evaluator application for AHK

![Build status badge](https://github.com/akosdudas/ahk-sample-evaluator/workflows/Build%20Docker%20images%20for%20AHK/badge.svg)

The `src` folder contains the source code for a sample evaluator application. The evaluator "core" is the [`Program.cs`](https://github.com/akosdudas/ahk-sample-evaluator/blob/master/src/evaluator/Program.cs) source code. For the CI, the evaluator application is [Dockerized](https://github.com/akosdudas/ahk-sample-evaluator/blob/master/src/evaluator/Dockerfile) as a standard .NET Core application.

The source code is build automatically in GitHub Actions and the resulting Docker image is uploaded to GitHub Container Registry to <https://github.com/users/akosdudas/packages/container/package/ahk-sample>. In order to push the Docker image login to GHCR is based on [GitHub Secret](https://help.github.com/en/actions/automating-your-workflow-with-github-actions/creating-and-using-encrypted-secrets). See the action descriptor file [here](https://github.com/akosdudas/ahk-sample-evaluator/blob/master/.github/workflows/ahk-build-images.yml).

The students receive a starter code, which lays out how the submission is expected. The sample for this started code is at <https://github.com/akosdudas/ahk-sample-startercode>.

The students push their solution to their own repository. A sample repository is available [here](https://github.com/akosdudas/ahk-sample-studentsolution). The process required of the student is as follows:

1. Student receives a dedicated GitHub repository with the starter code.
1. Student creates a new branch for the solution.
1. Student pushes commits to the new branch,
1. then submits the solution by opening a [pull request](https://github.com/akosdudas/ahk-sample-studentsolution/pull/1) inside her own repository,
1. which is then automatically evaluated and the result is commented into the pull request thread.
