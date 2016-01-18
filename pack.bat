packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Validator\Jal.Validator.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Validator.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Validator.Installer\Jal.Validator.Installer.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Validator.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Validator.FluentValidation\Jal.Validator.FluentValidation.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Validator.Nuget

pause;