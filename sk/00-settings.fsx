#r "nuget: Microsoft.SemanticKernel"
#I @"Plugins"
#load "Config.fs"

open sk
open Microsoft.SemanticKernel
open System.IO

let K =
    Kernel.CreateBuilder()
        .AddAzureOpenAIChatCompletion(
            deploymentName = Config.modelName,
            endpoint =  Config.endpoint,
            apiKey = Config.key)
                .Build()
let funPluginDirectoryPath = 
    Path.Combine( "sk","Plugins")
let plugins = K.ImportPluginFromPromptDirectory(funPluginDirectoryPath)

// Arguments conept: https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/Concepts/Functions/Arguments.cs
let arguments =
    KernelArguments(
        executionSettings = PromptExecutionSettings(
            ExtensionData = dict [("input", "time travel to dinosaur age")]))

let result = K.InvokeAsync(plugins["Joke"], arguments).Result

printfn "%A" result
