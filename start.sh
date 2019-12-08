cd 2019

mkdir $1
cd $1

dotnet new sln
dotnet new console --name $1.App
dotnet new xunit --name $1.Tests
dotnet sln $1.sln add **/*.csproj

dotnet add $1.Tests/$1.Tests.csproj reference $1.App/$1.App.csproj
dotnet add $1.Tests/$1.Tests.csproj package Shouldly

dotnet add $1.App/$1.App.csproj package Microsoft.Extensions.Configuration
dotnet add $1.App/$1.App.csproj package Microsoft.Extensions.Configuration.Abstractions
dotnet add $1.App/$1.App.csproj package Microsoft.Extensions.Configuration.CommandLine
dotnet add $1.App/$1.App.csproj package Microsoft.Extensions.Configuration.Json
dotnet add $1.App/$1.App.csproj package Microsoft.Extensions.Options.ConfigurationExtensions

cd $1.App

dotnet new aoc-starter --force --day $1

start ../$1.sln