#!/bin/sh -e

frameworkVersion="v4.0.30319"
frameworkPath="/c/Windows/Microsoft.NET/Framework/"

msbuildexe="${frameworkPath}${frameworkVersion}/msbuild.exe"

"${frameworkPath}${frameworkVersion}/msbuild.exe" build-targets.proj
