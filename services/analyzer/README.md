Анализаторы кода, упрощающие разрарботку по принятым на проекте подходам.

## Подключение
Для быстрого подключения ко всем проектам в солюшене, рекомендуется добавить файл `Directory.Build.props` в корень репозитория или папку `src` с содержимым:
```xml
<Project>
    <PropertyGroup>
        <WarningsNotAsErrors>AIR0001;AIR0002;AIR0003;</WarningsNotAsErrors>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Inno.Air.Analyzers" Version="*-*" PrivateAssets="all"/>
        <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.7.0" PrivateAssets="all"/>
    </ItemGroup>
</Project>
```

Также, стоит добавить в `.editorconfig` строчки:
```ini
dotnet_diagnostic.AIR0001.severity = suggestion
dotnet_diagnostic.AIR0002.severity = suggestion
dotnet_diagnostic.AIR0003.severity = suggestion
```
