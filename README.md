# Sample evaluator application for AHK

![Build status badge](https://github.com/akosdudas/ahk-sample-evaluator/workflows/Build%20Docker%20images%20for%20AHK/badge.svg)

The `src` folder contains the source code for a sample evaluator application. The evaluator "core" is the [`Program.cs`](https://github.com/akosdudas/ahk-sample-evaluator/blob/master/src/evaluator/Program.cs) source code. For the CI, the evaluator application is [Dockerized](https://github.com/akosdudas/ahk-sample-evaluator/blob/master/src/evaluator/Dockerfile) as a standard .NET Core application.

The source code is build automatically in GitHub Actions and the resulting Docker image is uploaded to Docker Hub to <https://hub.docker.com/r/akosdudas/ahk-sample>. Login to Docker Hub is based on [GitHub Secret](https://help.github.com/en/actions/automating-your-workflow-with-github-actions/creating-and-using-encrypted-secrets). See the action descriptor file [here](https://github.com/akosdudas/ahk-sample-evaluator/blob/master/.github/workflows/ahk-build-images.yml).
